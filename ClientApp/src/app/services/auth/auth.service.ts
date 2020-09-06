import { LoginResponse } from '../../models/login/login-response';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Login } from '../../models/login/login.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  urlRoot = environment.apiUrl;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, public jwtHelper: JwtHelperService) { }

  login(loginForm: Login): Observable<void> {
    const url = `${this.urlRoot}/auth/login`;
    return this.http.post(url, loginForm, this.httpOptions).pipe(
      map((data: LoginResponse) => {
        localStorage.setItem('access_token', data.access_token);
      })
    );
  }

  getEmail(): string {
    const token = localStorage.getItem('access_token');
    const decodeToken = this.jwtHelper.decodeToken(token);
    return decodeToken.email;
  }

  getRole(): string {
    const token = localStorage.getItem('access_token');
    const decodeToken = this.jwtHelper.decodeToken(token);
    return decodeToken.role;
  }

  getUniqueName(): string {
    const token = localStorage.getItem('access_token');
    const decodeToken = this.jwtHelper.decodeToken(token);
    return decodeToken.unique_name;
  }

  getAccessToken(): string {
    return localStorage.getItem('access_token');
  }

  logout(): void {
    localStorage.clear();
  }

  isTokenExpired(): boolean {
    return this.jwtHelper.isTokenExpired(localStorage.getItem('access_token'));
  }

  isLoggedIn(): boolean {
    return this.getAccessToken() != null;
  }

}
