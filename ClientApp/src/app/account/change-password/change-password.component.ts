import { ChangePassword } from '../../models/change-password/change-password.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  loading = false;
  changePasswordForm: FormGroup;
  hidePassword = true;
  hideConfirmPassword = true;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.changePasswordForm = this.fb.group({
      password: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]],
      passwordConfirm: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
    });
  }

  onSubmit(changePasswordForm: ChangePassword, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.authService.createAdminUser(changePasswordForm)
      .subscribe(
        data => {
          this.snackBar.open('註冊成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.changePasswordForm.reset();
          this.loading = false;
        },
        error => {
          this.snackBar.open('註冊失敗', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }

}
