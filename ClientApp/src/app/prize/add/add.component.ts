import { PrizeAdd } from './../../models/prize/prize-add.model';
import { PrizeService } from './../../services/prize.service';
import { FormBuilder, Validators, FormGroupDirective, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {

  roundId: string;
  addPrizeForm: FormGroup;

  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private prizeService: PrizeService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.roundId = params.id;
    });
    this.addPrizeForm = this.fb.group({
      name: [null, Validators.required],
      number: [null, Validators.required],
      order: [null, Validators.required]
    });
  }

  onSubmit(formDirective: FormGroupDirective, prize: PrizeAdd): void {
    this.prizeService.createPrize(this.roundId, prize)
      .subscribe(
        data => {
          formDirective.resetForm();
          this.addPrizeForm.reset();
        },
        error => {}
      );
  }

}
