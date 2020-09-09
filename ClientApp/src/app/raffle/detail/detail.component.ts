import { AttendeeAddComponent } from '../attendee-add/attendee-add.component';
import { AuthService } from '../../services/auth/auth.service';
import { UploadComponent } from '../upload/upload.component';
import { Component, OnInit } from '@angular/core';
import { RaffleService } from '../../services/raffle/raffle.service';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Item } from '../../models/item/item.model';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  loading = true;
  fetchDataError = false;
  itemId: string;
  item: Item;

  constructor(
    private activatedRoute: ActivatedRoute,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog,
    public authService: AuthService) { }

  ngOnInit(): void {
    this.getUrlId();
    this.initData();
  }

  showAddFormDialog(): void {
    this.dialog.open(AttendeeAddComponent, {
      data: this.itemId
    });
  }

  showUploadFormDialog(): void {
    this.dialog.open(UploadComponent, {
      data: this.itemId
    });
  }

  initData(): void {
    this.raffleService.getItemById(this.itemId)
      .subscribe(
        data => {
          this.item = data;
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

  getUrlId(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      this.itemId = params.get('itemId');
    });
  }

  reload(): void {
    this.loading = true;
    this.fetchDataError = false;
    this.initData();
  }

}
