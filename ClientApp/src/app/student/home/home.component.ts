import { StudentService } from '../../services/student/student.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Student } from 'src/app/models/student/student.model';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Student>();
  displayedColumns: string[] = ['nid', 'name', 'department'];
  fetchDataError = false;
  loading = true;
  roundId: string;

  constructor(
    private studentService: StudentService,
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
    this.studentService.getStudentsForRound(this.roundId)
      .subscribe(
        data => {
          this.dataSource.data = data;
          this.dataSource.paginator = this.paginator;
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

}
