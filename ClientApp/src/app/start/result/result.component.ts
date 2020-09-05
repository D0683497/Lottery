import { AttendeeService } from '../../services/attendee/attendee.service';
import { Component, OnInit, Inject } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { take, map } from 'rxjs/operators';
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
  count = 10;
  // showResult = false;
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
    // this.counter$ = timer(0, 1000).pipe(
    //   take(this.count),
    //   map(() => --this.count)
    // );
  }

  ngOnInit(): void {
    // this.counter$.subscribe(t => {
    //   if (t === 0) {
    //     // this.showResult = true;
    //   }
    // });
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
