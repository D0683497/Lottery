import { Component, OnInit } from '@angular/core';
import { RaffleService } from 'src/app/services/raffle.service';
import { ActivatedRoute } from '@angular/router';
import { Round } from 'src/app/models/round/round.model';
import { Observable } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map, shareReplay } from 'rxjs/operators';

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
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(
    private activatedRoute: ActivatedRoute,
    private breakpointObserver: BreakpointObserver,
    private raffleService: RaffleService) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      this.roundId = params.get('roundId');
    });
    this.raffleService.getRoundById(this.roundId)
      .subscribe(
        data => {
          this.round = data;
          this.fetchDataError = false;
          this.loading = false;
        },
        error => {
          this.fetchDataError = true;
          this.loading = false;
        }
      );
  }

}
