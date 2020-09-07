import { AuthService } from '../../services/auth/auth.service';
import { Login } from '../../models/login/login.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  pattern = new RegExp(/[\w\-\.\@\+\#\$\%\\\/\(\)\[\]\*\&\:\>\<\^\!\{\}\=]+/gm);
  loading = false;
  loginForm: FormGroup;
  hide = true;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private authService: AuthService,
    private router: Router) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      userName: [null, [Validators.required, Validators.pattern(this.pattern)]],
      password: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
    });
  }

  onSubmit(loginForm: Login, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.authService.login(loginForm)
      .subscribe(
        data => {
          this.snackBar.open('登入成功', '關閉', { duration: 5000 });
          this.router.navigate(['/']);
          formDirective.resetForm();
          this.loginForm.reset();
          this.loading = false;
        },
        error => {
          this.snackBar.open('登入失敗', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }
}
