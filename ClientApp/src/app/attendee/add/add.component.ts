import { AttendeeAdd } from '../../models/attendee/attendee-add.model';
import { AttendeeService } from '../../services/attendee/attendee.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  itemId: string;
  loading = false;
  addAttendeeForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private attendeeService: AttendeeService,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<AddComponent>,
    @Inject(MAT_DIALOG_DATA) private data: string) {
      this.itemId = data;
    }

  ngOnInit(): void {
    this.addAttendeeForm = this.fb.group({
      nid: [null, Validators.required],
      name: [null, Validators.required],
      department: [null, Validators.required]
    });
  }

  onSubmit(attendee: AttendeeAdd, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.attendeeService.createAttendeeForItemId(this.itemId, attendee)
      .subscribe(
        data => {
          this.snackBar.open('新增成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.addAttendeeForm.reset();
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
