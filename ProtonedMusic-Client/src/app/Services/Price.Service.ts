import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CurrencyConverterService } from './CurrencyConverter.Service';
import { IPGeolocationService } from './ip-geolocation.service';

@Injectable({
  providedIn: 'root'
})
export class PriceService {

  constructor(
    private http: HttpClient,
    private currencyConverterService: CurrencyConverterService,
    private ipGeolocation: IPGeolocationService
  ) { }

  async getPriceInLocalCurrency(product: any){
    try {
      const geoLocation = await this.ipGeolocation.getGeolocation().toPromise();
      console.log('Geolocation data:', geoLocation);

      const localCurrency = geoLocation.currency;
      console.log('Local currency:', localCurrency);

      const exchangeRates = await this.currencyConverterService.getExchangeRates().toPromise();
      console.log('Exchange rates:', exchangeRates);

      // convert rates to users locale rates
      this.currencyConverterService.setExchangeRates(exchangeRates);
      this.currencyConverterService.convertPrices([product], localCurrency);

      // return converted price
      const convertedPrice = this.currencyConverterService.formatCurrency(product.price, localCurrency);
      console.log('Converted price:', convertedPrice);
      return convertedPrice;
    } catch (error) {
      console.error('Error getting price in local currency', error);
      return product.price;
    }
  }
}
