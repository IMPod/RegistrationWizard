// src/app/services/countries.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {
  private countriesUrl = '/api/Countries';

  constructor(private http: HttpClient) { }

  getCountries(): Observable<any> {
    return this.http.get<any>(this.countriesUrl, { headers: { 'Cache-Control': 'no-cache' } });
  }

  getProvincesByCountry(countryId: number): Observable<any> {
    return this.http.get<any>(`${this.countriesUrl}/${countryId}/provinces`);
  }
}
