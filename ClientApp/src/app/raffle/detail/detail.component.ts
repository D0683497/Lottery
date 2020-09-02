import { Component, OnInit } from '@angular/core';
import { RaffleService } from '../../services/raffle/raffle.service';
import { ActivatedRoute } from '@angular/router';
import { Round } from 'src/app/models/round/round.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {

  loading = true;
  fetchDataError = true;
  roundId: string;
  round: Round;

  constructor(
    private activatedRoute: ActivatedRoute,
    private raffleService: RaffleService,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getUrlId();
    this.initData();
  }

  initData(): void {
    this.raffleService.getRoundById(this.roundId)
      .subscribe(
        data => {
          this.round = data;
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
      this.roundId = params.get('roundId');
    });
  }

}
