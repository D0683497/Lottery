import { Component, OnInit, Inject } from '@angular/core';
import { Attendee } from '../../models/attendee/attendee.model';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-draw-result',
  templateUrl: './draw-result.component.html',
  styleUrls: ['./draw-result.component.scss']
})
export class DrawResultComponent implements OnInit {

  attendee: Attendee;

  constructor(@Inject(MAT_DIALOG_DATA) private data: Attendee, private dialog: MatDialog,) {
    this.attendee = data;
  }

  ngOnInit(): void {
  }

  close(): void {
    this.dialog.closeAll();
  }

}
