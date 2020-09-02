import { RoundAdd } from '../../models/round/round-add.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroupDirective, FormGroup } from '@angular/forms';

import { RaffleService } from '../../services/raffle/raffle.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  loading = false;
  addRaffleForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.addRaffleForm = this.fb.group({
      name: [null, Validators.required]
    });
  }

  onSubmit(round: RoundAdd, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.raffleService.createRound(round)
      .subscribe(
        data => {
          this.snackBar.open('新增成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.addRaffleForm.reset();
          this.loading = false;
        },
        error => {
          this.snackBar.open('發生錯誤', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }
}
