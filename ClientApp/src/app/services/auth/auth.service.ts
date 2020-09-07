import { Register } from '../../models/register/register.model';
import { LoginResponse } from '../../models/login/login-response';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Login } from '../../models/login/login.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  urlRoot = environment.apiUrl;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService, private router: Router) { }

  // 登入
  login(loginForm: Login): Observable<void> {
    const url = `${this.urlRoot}/auth/login`;
    return this.http.post(url, loginForm, this.httpOptions).pipe(
      map((data: LoginResponse) => localStorage.setItem('access_token', data.access_token))
    );
  }

  createAdminUser(registerForm: Register): Observable<object> {
    const url = `${this.urlRoot}/account/register/admin`;
    return this.http.post(url, registerForm, this.httpOptions);
  }

  createHostUser(registerForm: Register): Observable<object> {
    const url = `${this.urlRoot}/account/register/host`;
    return this.http.post(url, registerForm, this.httpOptions);
  }

  createClientUser(registerForm: Register): Observable<object> {
    const url = `${this.urlRoot}/account/register/client`;
    return this.http.post(url, registerForm, this.httpOptions);
  }

  // 登出
  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }

  // 獲取角色
  getRole(): string {
    const token = localStorage.getItem('access_token');
    const decodeToken = this.jwtHelper.decodeToken(token);
    return decodeToken.role;
  }

  // 獲取用戶名稱
  getUniqueName(): string {
    const token = localStorage.getItem('access_token');
    const decodeToken = this.jwtHelper.decodeToken(token);
    return decodeToken.unique_name;
  }

  // Token 是否過期
  isTokenExpired(): boolean {
    return this.jwtHelper.isTokenExpired(localStorage.getItem('access_token'));
  }

  // 是否登入
  isLoggedIn(): boolean {
    return localStorage.getItem('access_token') != null;
  }

}
