import { Component, OnInit } from '@angular/core';
import { IPGeolocationService } from './Services/ip-geolocation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ProtonedMusic-Client';
  currencyDetectionResult: string = '';
  exchangeRates: { [key: string]: number } = {
    USD: 0.15,
    EUR: 0.13,
    JPY: 17.01,
    KRW: 186.38,
    CNY: 0.97,
    GBP: 0.12,
    SGD: 0.20,

  };

  constructor(private IPGeolocationService: IPGeolocationService) { }

  ngOnInit(): void {
    this.IPGeolocationService.getGeolocation().subscribe(
      (data: any) => {
        const country = data.countryCode;
        console.log('Country:', country);

        // Map country code to currency
        let currency;
        switch (country) {
          case 'US':
            currency = 'USD';
            break;
          case 'FR':
          case 'ES':
          case 'DE':
          case 'IT':
            currency = 'EUR';
            break;
          case 'JP':
            currency = 'JPY';
            break;
          case 'KR':
            currency = 'KRW';
            break;
          case 'CN':
            currency = 'CNY';
            break;
          case 'GB':
            currency = 'GBP';
            break;
          case 'SG':
            currency = 'SGD';
            break;
          default:
            currency = 'DKK';
            break;
        }
        console.log('Currency:', currency);
        this.currencyDetectionResult = currency;
      },
      error => {
        console.error('Error getting geolocation:', error);
        // Fallback to default currency
        this.currencyDetectionResult = 'DKK';
      }
    );
  }

  convertPriceToCurrency(price: number, targetCurrency: string): number {
    const rate = this.exchangeRates[targetCurrency];
    if (rate) {
      return price * rate;
    } else {
      console.error('Target currency not supported for conversion.');
      return price; // Returnerer den oprindelige pris, hvis valutaen ikke er understøttet
    }
  }
}
