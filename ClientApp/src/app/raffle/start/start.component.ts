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
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  start(): void {
    this.starting = true;
  }

  startStudent(): void {
    this.starting = true;
    this.openDialog();
    this.starting = false;
  }

  startStaff(): void {
    console.log('staff');
  }

}

/* https://animate.style */

/* https://github.com/JonLim/three-trackballcontrols */


/* 官網 https://threejs.org/ */
/* 範例 demo https://threejs.org/examples/#css3d_periodictable */
/* 範例 code https://github.com/mrdoob/three.js/blob/master/examples/css3d_periodictable.html */
/* https://github.com/JohnnyDevNull/ng-three-template */
/* https://github.com/omidsakhi/ng-three-periodictable */


/* https://github.com/tweenjs/tween.js/ */
/* ES6 版本 https://github.com/tweenjs/es6-tween */
