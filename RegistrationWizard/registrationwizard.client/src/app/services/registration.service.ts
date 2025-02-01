// src/app/services/registration.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private apiUrl = '/api/registration'; 

  constructor(private http: HttpClient) { }

  register(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data);
  }
}
