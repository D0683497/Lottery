import { AttendeeService } from '../../services/attendee/attendee.service';
import { DrawResultComponent } from '../draw-result/draw-result.component';
import { Item } from '../../models/item/item.model';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  loading = true;
  fetchDataError = false;
  items: Item[];
  starting = false;

  constructor(
    private dialog: MatDialog,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private attendeeService: AttendeeService) { }

  ngOnInit(): void {
    this.initData();
  }

  initData(): void {
    this.raffleService.getAllItems()
      .subscribe(
        data => {
          this.items = data;
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

  reload(): void {
    this.loading = true;
    this.fetchDataError = false;
    this.initData();
  }

  startDraw(itemId: string): void {
    this.snackBar.open('抽獎中', '關閉', { duration: 1000 });
    this.attendeeService.getAttendeeRandomForItemId(itemId)
      .subscribe(
        data => {
          this.dialog.open(DrawResultComponent, {
            disableClose: true,
            backdropClass: 'backdropBackground',
            data
          });
        },
        error => {
          this.snackBar.open('抽獎失敗', '關閉', { duration: 5000 });
        }
      );
  }

}
