import { Item } from '../../models/item/item.model';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ResultComponent } from '../result/result.component';
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
  // roundId: string;
  starting = false;

  constructor(
    private activatedRoute: ActivatedRoute,
    private dialog: MatDialog,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar) { }

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
    console.log(itemId);
    const dialogRef = this.dialog.open(ResultComponent, {
      height: 'calc(100% - 50px)',
      width: 'calc(100% - 50px)',
      maxWidth: '100%',
      maxHeight: '100%',
      disableClose: true,
      data: itemId
    });
    dialogRef.afterClosed().subscribe(() => {
      // this.reload();
    });
  }

  // openDialog(method: string): void {
  //   const dialogRef = this.dialog.open(ResultComponent, {
  //     height: '90%',
  //     width: '80%',
  //     backdropClass: 'backdropBackground',
  //     data: {
  //       roundId: this.roundId,
  //       method
  //     }
  //   });
  //   dialogRef.afterClosed().subscribe(() => {
  //     this.starting = false;
  //   });
  // }

  // startStudent(): void {
  //   this.starting = true;
  //   this.openDialog('student');
  // }

  // startStaff(): void {
  //   this.starting = true;
  //   this.openDialog('staff');
  // }

}
