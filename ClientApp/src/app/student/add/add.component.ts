import { StudentService } from '../../services/student/student.service';
import { StudentAdd } from '../../models/student/student-add.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  roundId: string;
  loading = false;
  addStudentForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private studentService: StudentService,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getUrlId();
    this.addStudentForm = this.fb.group({
      nid: [null, Validators.required],
      name: [null, Validators.required],
      department: [null, Validators.required]
    });
  }

  getUrlId(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.roundId = params.id;
    });
  }

  onSubmit(student: StudentAdd, formDirective: FormGroupDirective): void {
    this.loading = true;
    this.studentService.createStudentForRound(this.roundId, student)
      .subscribe(
        data => {
          this.snackBar.open('新增成功', '關閉', { duration: 5000 });
          formDirective.resetForm();
          this.addStudentForm.reset();
          this.loading = false;
        },
        error => {
          this.snackBar.open('發生錯誤', '關閉', { duration: 5000 });
          this.loading = false;
        }
      );
  }

}
