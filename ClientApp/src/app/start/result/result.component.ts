import { StaffService } from '../../services/staff/staff.service';
import { StudentService } from '../../services/student/student.service';
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

  constructor(
    private studentService: StudentService,
    private staffService: StaffService,
    @Inject(MAT_DIALOG_DATA) private data: any) {
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
    this.getStaffData();
  }

  getStaffData(): void {
    this.staffService.getRandomStaffForRound(this.roundId)
      .subscribe(
        data => {
          this.nid = data.nid;
          this.name = data.name;
          this.department = data.department;
        },
        error => {}
      );
  }

}
