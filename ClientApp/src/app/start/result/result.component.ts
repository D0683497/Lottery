import { AttendeeService } from '../../services/attendee/attendee.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Attendee } from 'src/app/models/attendee/attendee.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  counter$: Observable<number>;
  loading = true;
  fetchDataError = false;
  fetchDataNoContent = false;
  itemId: string;
  attendee: Attendee;

  constructor(
    @Inject(MAT_DIALOG_DATA) private data: string,
    private attendeeService: AttendeeService,
    private snackBar: MatSnackBar) {
    this.itemId = data;
  }

  ngOnInit(): void {
    this.initData();
  }

  initData(): void {
    this.attendeeService.getAttendeeRandomForItemId(this.itemId)
      .subscribe(
        data => {
          if (data == null) {
            this.fetchDataNoContent = true;
          }
          this.attendee = data;
          this.fetchDataError = false;
          this.loading = false;
        },
        error => {
          this.snackBar.open('發生錯誤', '關閉', { duration: 5000 });
          this.fetchDataError = true;
          this.loading = false;
        }
      );
  }

}
