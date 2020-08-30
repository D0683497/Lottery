import { AttendeeService } from './../../services/attendee.service';
import { AttendeeAdd } from './../../models/attendee/attendee-add.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  roundId: string;
  addAttendeeForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private attendeeService: AttendeeService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.roundId = params.id;
    });
    this.addAttendeeForm = this.fb.group({
      nid: [null, Validators.required],
      name: [null, Validators.required],
      department: [null, Validators.required]
    });
  }

  onSubmit(formDirective: FormGroupDirective, attendee: AttendeeAdd): void {
    this.attendeeService.createAttendee(this.roundId, attendee)
      .subscribe(
        data => {
          formDirective.resetForm();
          this.addAttendeeForm.reset();
        },
        error => {}
      );
  }

}
