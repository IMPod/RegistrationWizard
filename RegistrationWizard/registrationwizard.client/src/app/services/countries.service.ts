// src/app/services/countries.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { retryWhen, delay, take, concatMap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {
  private countriesUrl = '/api/Countries';

  constructor(private http: HttpClient) { }

  getCountries(): Observable<{ data: Country[] }> {
    return this.http
      .get<{ data: Country[] }>(this.countriesUrl, {
        headers: { 'Cache-Control': 'no-cache' }
      })
      .pipe(
        retryWhen(errors =>
          errors.pipe(
            take(5),
            delay(2000),
            concatMap((error, index) => {
              if (index === 4) {
                return throwError(() => error);
              }
              return of(error);
            })
          )
        ),
        catchError((error) => {
          console.error('Error loading countries:', error);
          return throwError(() => error);
        })
      );
  }

  getProvincesByCountry(countryId: number): Observable<{ data: Province[] }> {
    return this.http.get<{ data: Province[] }>(`${this.countriesUrl}/${countryId}/provinces`);
  }
}

export interface Country {
  id: number;
  name: string;

}
export interface Province {
  id: number;
  name: string;
}
