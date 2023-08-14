import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { AuthenticationResponse } from 'src/app/models/authentication-response.model';
import { Login } from 'src/app/models/login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpService: HttpService) { }

  public RequestMagicLink(login : Login): Observable<AuthenticationResponse>{
    return this.httpService.post('Authentication/login', login);
  }

  public GetProfile(): Observable<AuthenticationResponse>{
    return this.httpService.get('Authentication/get-profile');
  }
}
