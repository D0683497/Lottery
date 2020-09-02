import { StaffService } from '../../services/staff/staff.service';
import { Staff } from '../../models/staff/staff.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Staff>();

  displayedColumns: string[] = ['nid', 'name', 'department'];

  fetchDataError = true;

  loading = true;

  roundId: string;

  constructor(
    private staffService: StaffService,
    private snackBar: MatSnackBar,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.getUrlId();
    this.initData();
  }

  getUrlId(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.roundId = params.id;
    });
  }

  initData(): void {
    this.staffService.getStaffsForRound(this.roundId)
      .subscribe(
        data => {
          this.dataSource.data = data;
          this.dataSource.paginator = this.paginator;
          this.fetchDataError = false;
          this.loading = false;
        },
        error => {
          this.snackBar.open('獲取資料失敗', '關閉', { duration: 10000 });
          this.fetchDataError = true;
          this.loading = false;
        }
      );
  }

}
