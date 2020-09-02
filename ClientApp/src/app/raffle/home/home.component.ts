import { RaffleService } from '../../services/raffle/raffle.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Round } from 'src/app/models/round/round.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Round>();
  displayedColumns: string[] = ['id', 'name', 'action'];
  fetchDataError = true;
  loading = true;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private raffleService: RaffleService,
    private snackBar: MatSnackBar,
    private breakpointObserver: BreakpointObserver) { }

  ngOnInit(): void {
    this.initData();
  }

  initData(): void {
    this.raffleService.getRounds()
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


