import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CurrencyConverterService {
  private apiUrl = 'https://api.exchangerate-api.com/v4/latest/DKK'; // API til at hente valutakurser for DKK
  public exchangeRates: any; // Opbevarer valutakurserne her

  constructor(private http: HttpClient) {}

  getExchangeRates() {
    return this.http.get(this.apiUrl);
  }

  setExchangeRates(rates: any) {
    this.exchangeRates = rates;
  }

  convertPrices(products: any[], targetCurrency: string) {
    if (!this.exchangeRates) {
      console.error('Exchange rates not available.');
      return;
    }

    products.forEach(product => {
      const rate = this.exchangeRates[targetCurrency];
      if (rate) {
        product.price = product.price * rate;
      } else {
        console.error('Target currency not supported for conversion.');
      }
    });
  }

  formatCurrency(amount: number, currency: string): string {
    // Implementer logik for at formatere valutaen efter behov
    // F.eks. tilføj valutasymbol og antal decimaler
    return amount.toLocaleString('da-DK') + ' ' + currency;
  }
}
