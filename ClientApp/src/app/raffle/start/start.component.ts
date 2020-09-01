import { Component, OnInit } from '@angular/core';

import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ResultComponent } from '../result/result.component';

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {

  // roundId: string;
  starting = false;

  constructor(private activatedRoute: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit(): void {
    // this.activatedRoute.paramMap.subscribe(params => {
    //   this.roundId = params.get('roundId');
    // });
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ResultComponent, {
      height: '90%',
      width: '80%',
      backdropClass: 'backdropBackground'
    });
    dialogRef.afterClosed().subscribe(() => {
      this.starting = false;
    });
  }

  startStudent(): void {
    this.starting = true;
    this.openDialog();
  }

  startStaff(): void {
    this.starting = true;
    this.openDialog();
  }

}
