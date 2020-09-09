import { Item } from '../../models/item/item.model';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from '../../../environments/environment';
import * as signalR from '@microsoft/signalr';
import { AttendeeService } from '../../services/attendee/attendee.service';

@Component({
  selector: 'app-host',
  templateUrl: './host.component.html',
  styleUrls: ['./host.component.scss']
})
export class HostComponent implements OnInit, OnDestroy {

  urlRoot = environment.apiUrl;
  loading = true;
  fetchDataError = false;
  items: Item[];
  connection: signalR.HubConnection;

  constructor(
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private attendeeService: AttendeeService) {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(`${this.urlRoot}/lottery`)
        .build();

      this.connection.start().catch(err => {
        this.snackBar.open('連線錯誤', '關閉', { duration: 5000 });
      });
    }

  ngOnInit(): void {
    this.initData();
  }

  ngOnDestroy(): void {
    this.connection.onclose(() => {});
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
    this.snackBar.open('抽獎中', '關閉', { duration: 1500, verticalPosition: 'top' });

    this.attendeeService.getAttendeeRandomForItemId(itemId)
      .subscribe(
        data => {
          this.connection.invoke('SendMessage', data)
            .catch((error) => {
              this.snackBar.open('連線錯誤', '關閉', { duration: 5000 });
            });
        },
        error => {
          this.snackBar.open('抽獎失敗', '關閉', { duration: 5000 });
        }
      );
  }

}
