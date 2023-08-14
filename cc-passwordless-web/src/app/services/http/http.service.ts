import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  BASE_URL_SERVICES = 'https://localhost:7105/api/';

  constructor(private http: HttpClient) { }

  private getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    
    const token = localStorage.getItem('token');
    if (token) {
      headers = headers.append('Authorization', `Bearer ${token}`);
    }
    
    return headers;
  }

  public get<T>(
    url: string,
    urlParams?: any[]
  ): Observable<T> {
    let headers = this.getHeaders();
    var URL = this.getServiceURL() + url;
    if (urlParams != undefined) {
      URL += this.buildUrl(urlParams);
    }
    return this.http.get<T>(URL, { headers });
  }

  public post<T>(
    url: string,
    body: any,
    urlParams?: any[]
  ): Observable<T> {
    let headers = this.getHeaders();
    headers = headers.append('Content-Type', 'application/json');
    var URL = this.getServiceURL() + url;
    if (urlParams != undefined) {
      URL += this.buildUrl(urlParams);
    }
    return this.http.post<T>(URL, JSON.stringify(body), { headers });
  }

  getServiceURL(): string {
    return this.BASE_URL_SERVICES;
  }

  buildUrl(urlParams: any[]): string {
    return '/' + urlParams.join('/');
  }
}
