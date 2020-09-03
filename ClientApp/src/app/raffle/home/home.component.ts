import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent, MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Item } from '../../models/item/item.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Item>();
  pageIndex = 0;
  pageSize = 10;
  pageLength: number;
  pageSizeOptions: number[] = [10, 20, 30, 40, 50];
  displayedColumns: string[] = ['id', 'name', 'action'];
  fetchDataError = false;
  loading = true;

  constructor(
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private matPaginatorIntl: MatPaginatorIntl) { }

  ngOnInit(): void {
    this.getData();
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getData();
  }

  getData(): void {
    this.raffleService.getAllItemsLength()
      .subscribe(
        data => {
          this.pageLength = data;
        },
        error => {}
      );
    this.raffleService.getItems(this.pageIndex + 1, this.pageSize)
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


