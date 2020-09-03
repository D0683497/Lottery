import { StaffService } from '../../services/staff/staff.service';
import { Staff } from '../../models/staff/staff.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent, MatPaginatorIntl } from '@angular/material/paginator';
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

  dataSource = new MatTableDataSource<Staff>();
  pageIndex = 0;
  pageSize = 10;
  pageLength: number;
  pageSizeOptions: number[] = [10, 20, 30, 40, 50];
  displayedColumns: string[] = ['nid', 'name', 'department'];
  fetchDataError = false;
  loading = true;
  roundId: string;

  constructor(
    private staffService: StaffService,
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
    this.staffService.getStaffsLengthForRound(this.roundId)
      .subscribe(
        data => {
          this.pageLength = data;
        },
        error => {}
      );
    this.staffService.getStaffsForRound(this.roundId, this.pageIndex + 1, this.pageSize)
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
      this.roundId = params.id;
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
