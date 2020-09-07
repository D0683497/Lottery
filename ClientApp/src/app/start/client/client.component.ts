import { Item } from '../../models/item/item.model';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ResultComponent } from '../result/result.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from '../../../environments/environment';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss']
})
export class ClientComponent implements OnInit {

  urlRoot = environment.apiUrl;
  loading = true;
  fetchDataError = false;
  items: Item[];
  starting = false;
  connection: signalR.HubConnection;

  constructor(
    private dialog: MatDialog,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(`${this.urlRoot}/lottery`)
        .withAutomaticReconnect()
        .build();

      this.connection.on('messageReceived', (data) => {
        this.startDraw(data);
      });

      this.connection.start().catch(err => {
        this.snackBar.open('連線錯誤', '關閉', { duration: 5000 });
      });
    }

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

}
