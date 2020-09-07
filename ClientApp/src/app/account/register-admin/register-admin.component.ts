import { Register } from '../../models/register/register.model';
import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup, FormGroupDirective } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-register-admin',
  templateUrl: './register-admin.component.html',
  styleUrls: ['./register-admin.component.scss']
})
export class RegisterAdminComponent implements OnInit {

  loading = false;
  registerForm: FormGroup;
  hidePassword = true;
  hideConfirmPassword = true;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      userName: [null, Validators.required],
      password: [null, Validators.required, Validators.minLength(8), Validators.maxLength(64)],
      passwordConfirm: [null, Validators.required, Validators.minLength(8), Validators.maxLength(64)],
      phoneNumber: null
    });
  }

  onSubmit(registerForm: Register, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.authService.createAdminUser(registerForm)
      .subscribe(
        data => {
          this.snackBar.open('註冊成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.registerForm.reset();
          this.loading = false;
        },
        error => {
          this.snackBar.open('註冊失敗', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }

}
