import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router, private snackBar: MatSnackBar) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (!this.authService.isLoggedIn()) {
      this.snackBar.open('請先登入', '關閉', { duration: 5000 });
      this.router.navigate(['/account/login']);
      return false;
    }
    if (this.authService.isTokenExpired()) {
      this.snackBar.open('請重新登入', '關閉', { duration: 5000 });
      this.router.navigate(['/account/login']);
      return false;
    }
    return true;
  }

}
