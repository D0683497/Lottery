import { AttendeeService } from '../../services/attendee/attendee.service';
import { Attendee } from '../../models/attendee/attendee.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

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

  constructor(
    private attendeeService: AttendeeService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute,
    private matPaginatorIntl: MatPaginatorIntl) { }

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
