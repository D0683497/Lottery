import { Component, OnInit } from '@angular/core';
import { Observable, timer } from 'rxjs';
import { take, map } from 'rxjs/operators';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html',
  styleUrls: ['./result.component.scss']
})
export class ResultComponent implements OnInit {

  counter$: Observable<number>;
  count = 10;
  showResult = false;

  constructor() {
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
  }

}
