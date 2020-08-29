import { RaffleService } from './../../services/raffle.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Round } from 'src/app/models/round/round.model';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { shareReplay, map } from 'rxjs/operators';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { AddComponent } from '../add/add.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource<Round>();
  displayedColumns: string[] = ['name', 'complete', 'action'];
  fetchDataError = true;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private raffleService: RaffleService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.raffleService.getRounds()
      .subscribe(
        data => {
          this.dataSource.data = data;
          this.dataSource.paginator = this.paginator;
          this.fetchDataError = false;
        },
        error => {
          console.warn(error.status);
          this.fetchDataError = true;
        }
      );
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(AddComponent);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

}


