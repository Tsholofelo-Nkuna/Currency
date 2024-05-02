import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CurrencyDto } from '../models/dataTransferObjects/currency.dto';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

 
  constructor(private _httpClient: HttpClient) {

  }

  get serviceUrl(){
    return 'https://localhost:7233/api/Currency';
  }

  getCurrencies(): Observable<CurrencyDto[]>{
    return this._httpClient.get<CurrencyDto[]>(this.serviceUrl);
  }

  addCurrency(currency: CurrencyDto): Observable<CurrencyDto>{
    return this._httpClient.post<CurrencyDto>(this.serviceUrl,currency);
  }

  getSavedCurrencies(): Observable<CurrencyDto[]>{
    return this._httpClient.get<CurrencyDto[]>(`${this.serviceUrl}/Saved`);
  }

  deleteCurrency(id: number): Observable<CurrencyDto>{
    return this._httpClient.delete<CurrencyDto>(`${this.serviceUrl}/${id}`);
  }

  updateCurrency(currency: CurrencyDto): Observable<CurrencyDto>{
    return this._httpClient.put<CurrencyDto>(this.serviceUrl, currency);
  }
}
