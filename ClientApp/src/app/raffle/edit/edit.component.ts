import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Round } from 'src/app/models/round/round.model';
import { Observable } from 'rxjs';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { shareReplay, map } from 'rxjs/operators';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  loading = true;
  fetchDataError = true;
  roundId: string;
  round: Round;
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
  editForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private breakpointObserver: BreakpointObserver,
    private raffleService: RaffleService,
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe(params => {
      this.roundId = params.get('roundId');
    });
    this.raffleService.getRoundById(this.roundId)
      .subscribe(
        data => {
          this.round = data;
          this.editForm = this.fb.group({
            name: [this.round.name, Validators.required]
          });
          this.fetchDataError = false;
          this.loading = false;
        },
        error => {
          this.fetchDataError = true;
          this.loading = false;
        }
      );
  }

  onSubmit(): void {
    alert('Thanks!');
  }

}
