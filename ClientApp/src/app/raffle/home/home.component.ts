import { RaffleService } from './../../services/raffle.service';
import { Component, OnInit } from '@angular/core';
import { Round } from 'src/app/models/round/round.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  dataSource: Round[];
  displayedColumns: string[] = ['id', 'name', 'complete'];

  constructor(private raffleService: RaffleService) { }

  ngOnInit(): void {
    this.raffleService.getRounds()
      .subscribe(
        data => {
          this.dataSource = data;
        },
        error => {}
      );
  }

}


