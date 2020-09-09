import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RaffleService } from '../../services/raffle/raffle.service';
import { ItemAdd } from '../../models/item/item-add.model';

@Component({
  selector: 'app-item-add',
  templateUrl: './item-add.component.html',
  styleUrls: ['./item-add.component.scss']
})
export class ItemAddComponent implements OnInit {

  loading = false;
  addRaffleForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<ItemAddComponent>) {}

  ngOnInit(): void {
    this.addRaffleForm = this.fb.group({
      name: [null, Validators.required]
    });
  }

  onSubmit(item: ItemAdd, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.raffleService.createItem(item)
      .subscribe(
        data => {
          this.snackBar.open('新增成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.addRaffleForm.reset();
          this.loading = false;
          this.dialogRef.close();
        },
        error => {
          this.snackBar.open('發生錯誤', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }

}
