import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Attendee } from '../../models/attendee/attendee.model';
import { Item } from '../../models/item/item.model';
import { AttendeeService } from '../../services/attendee/attendee.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { RaffleService } from '../../services/raffle/raffle.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-attendee',
  templateUrl: './attendee.component.html',
  styleUrls: ['./attendee.component.scss']
})
export class AttendeeComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Attendee>();
  pageIndex = 0;
  pageSize = 10;
  pageLength: number;
  pageSizeOptions: number[] = [10, 20, 30, 40, 50];
  displayedColumns: string[] = ['id', 'nid', 'name', 'department', 'isAwarded'];
  fetchDataError = false;
  loading = true;
  itemId: string;
  item: Item;

  constructor(
    private attendeeService: AttendeeService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private matPaginatorIntl: MatPaginatorIntl,
    private raffleService: RaffleService) { }

  ngOnInit(): void {
    this.getUrlId();
    this.getData();
    this.initPaginator();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getData();
  }

  getData(): void {
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
    this.attendeeService.getAllAttendeesLengthForItemId(this.itemId)
      .subscribe(
        data => {
          this.pageLength = data;
        },
        error => {}
      );
    this.attendeeService.getAttendeesForItemId(this.itemId, this.pageIndex + 1, this.pageSize)
      .subscribe(
        data => {
          this.dataSource.data = data;
          this.fetchDataError = false;
          this.loading = false;
        },
        error => {
          this.snackBar.open('獲取資料失敗', '關閉', { duration: 5000 });
          this.fetchDataError = true;
          this.loading = false;
        }
      );
  }

  reload(): void {
    this.fetchDataError = false;
    this.loading = true;
    this.getData();
  }

  exportXlsx(): void {
    this.attendeeService.getAttendeesXlsxForItemId(this.itemId)
      .subscribe(
        data => {
          saveAs(data, `${this.item.name}.xlsx`);
          this.snackBar.open('下載成功', '關閉', { duration: 5000 });
        },
        error => {
          this.snackBar.open('下載失敗', '關閉', { duration: 5000 });
        }
      );
  }

  exportCsv(): void {
    this.attendeeService.getAttendeesCsvForItemId(this.itemId)
      .subscribe(
        data => {
          saveAs(data, `${this.item.name}.csv`);
          this.snackBar.open('下載成功', '關閉', { duration: 5000 });
        },
        error => {
          this.snackBar.open('下載失敗', '關閉', { duration: 5000 });
        }
      );
  }

  exportJson(): void {
    this.attendeeService.getAttendeesJsonForItemId(this.itemId)
      .subscribe(
        data => {
          saveAs(data, `${this.item.name}.json`);
          this.snackBar.open('下載成功', '關閉', { duration: 5000 });
        },
        error => {
          this.snackBar.open('下載失敗', '關閉', { duration: 5000 });
        }
      );
  }

  getUrlId(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.itemId = params.id;
    });
  }

  initPaginator(): void {
    this.matPaginatorIntl.getRangeLabel = (page: number, pageSize: number, length: number): string => {
      return `第 ${page + 1} / ${Math.ceil(length / pageSize)} 頁`;
    };
    this.matPaginatorIntl.itemsPerPageLabel = '每頁筆數：';
    this.matPaginatorIntl.nextPageLabel = '下一頁';
    this.matPaginatorIntl.previousPageLabel = '上一頁';
    this.matPaginatorIntl.firstPageLabel = '第一頁';
    this.matPaginatorIntl.lastPageLabel = '最後一頁';
  }

}
