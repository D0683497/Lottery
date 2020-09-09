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
  hideOldPassword = true;
  hideNewPassword = true;
  hideConfirmPassword = true;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.changePasswordForm = this.fb.group({
      passwordOld: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]],
      passwordNew: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]],
      passwordConfirm: [null, [Validators.required, Validators.minLength(8), Validators.maxLength(64)]]
    });
  }

  onSubmit(changePasswordForm: any, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.authService.changePassword(changePasswordForm.passwordOld, changePasswordForm.passwordNew)
      .subscribe(
        data => {
          this.snackBar.open('修改成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.changePasswordForm.reset();
          this.loading = false;
          this.authService.logout();
        },
        error => {
          this.snackBar.open('修改失敗', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }

}
