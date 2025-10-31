import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'http://localhost:5000/api';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${API_URL}${endpoint}`, { headers: this.headers });
  }

  post<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${API_URL}${endpoint}`, data, { headers: this.headers });
  }

  put<T>(endpoint: string, data: any): Observable<T> {
    return this.http.put<T>(`${API_URL}${endpoint}`, data, { headers: this.headers });
  }

  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${API_URL}${endpoint}`, { headers: this.headers });
  }
}

