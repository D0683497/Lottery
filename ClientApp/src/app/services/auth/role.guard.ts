import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private authService: AuthService, private snackBar: MatSnackBar) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const activateRole = next.data.allowRole;
    const role = this.authService.getRole();
    if (activateRole.indexOf(role) === -1) {
      this.snackBar.open('沒有權限', '關閉', { duration: 5000 });
      return false;
    }
    return true;
  }

}
