import { Component, OnInit, Inject } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  counter$: Observable<number>;
  count = 10;
  showResult = false;

  roundId: string;
  nid: string;
  name: string;
  department: string;

  constructor(@Inject(MAT_DIALOG_DATA) private data: any) {
    this.roundId = data.roundId;
    this.counter$ = timer(0, 1000).pipe(
      take(this.count),
      map(() => --this.count)
    );
  }

  ngOnInit(): void {
    this.counter$.subscribe(t => {
      if (t === 0) {
        this.showResult = true;
      }
    });
    switch (this.data.method) {
      case 'student':
        this.getStudentData();
        break;
      case 'staff':
        this.getStaffData();
        break;
      default:
        break;
    }
  }

  getStaffData(): void {

  }

  getStudentData(): void {

  }

}
