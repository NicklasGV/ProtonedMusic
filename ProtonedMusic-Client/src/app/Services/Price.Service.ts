import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PriceService {
  private apiUrl = 'http://ip-api.com/json';

  constructor(private http: HttpClient) { }

  async getPriceInLocalCurrency(product: any){
    try {
      const geoLocation = await this.getGeolocation().toPromise();
      console.log('Geolocation data:', geoLocation);

      const localCurrency = this.getCountryCurrency(geoLocation.countryCode);
      console.log('Local currency:', localCurrency);

      const exchangeRates = await this.getExchangeRates().toPromise();
      console.log('Exchange rates:', exchangeRates);

      const convertedPrice = this.convertPrice(product.price, localCurrency, exchangeRates);
      console.log('Converted price:', convertedPrice);
      return convertedPrice;
    } catch (error) {
      console.error('Error getting price in local currency', error);
      return product.price;
    }
  }

  public getGeolocation() {
    return this.http.get<any>(`${this.apiUrl}`).pipe(
      catchError((error) => {
        console.error('Error getting geolocation:', error);
        return throwError(error);
      })
    );
  }

  public getCountryCurrency(countryCode: string): string {
    switch (countryCode) {
      case 'US':
        return 'USD';
      case 'FR':
      case 'ES':
      case 'DE':
      case 'IT':
        return 'EUR';
      case 'JP':
        return 'JPY';
      case 'KR':
        return 'KRW';
      case 'CN':
        return 'CNY';
      case 'GB':
        return 'GBP';
      case 'SG':
        return 'SGD';
      default:
        return 'DKK';
    }
  }

  private getExchangeRates() {
    return this.http.get<any>('https://api.exchangerate-api.com/v4/latest/DKK');
  }

  private convertPrice(price: number, targetCurrency: string, exchangeRates: any): number {
    const rate = exchangeRates[targetCurrency];
    if (rate) {
      return price * rate;
    } else {
      console.error('Target currency not supported for conversion.');
      return price;
    }
  }
}
