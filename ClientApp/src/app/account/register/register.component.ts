import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{

  registerForm: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      email: [null, [Validators.required, Validators.email]],
      userName: [null, Validators.required],
      password: [null, Validators.required],
      passwordConfirm: [null, Validators.required],
      phoneNumber: null
    });
  }

  onSubmit(): void {
    alert('Thanks!');
  }
}
