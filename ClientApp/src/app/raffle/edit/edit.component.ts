import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RaffleService } from '../../services/raffle/raffle.service';
import { Round } from 'src/app/models/round/round.model';
import { BreakpointObserver } from '@angular/cdk/layout';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {

  loading = true;
  fetchDataError = false;
  roundId: string;
  round: Round;
  editForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private raffleService: RaffleService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getUrlId();
    this.initData();
  }

  onSubmit(): void {
    alert('Thanks!');
  }

  initData(): void {
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
