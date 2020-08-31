import { PrizeAdd } from '../models/prize/prize-add.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Prize } from '../models/prize/prize.model';
import { Observable } from 'rxjs';
import { MessageService } from './message.service';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PrizeService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private messageService: MessageService) { }

  getPrizesForRound(roundId: string): Observable<Prize[]> {
    const url = `${this.urlRoot}/rounds/${roundId}/prizes`;
    return this.http.get<Prize[]>(url, this.httpOptions).pipe(
      tap(_ => this.log('fetched rounds')),
    );
  }

  createPrize(roundId: string, prize: PrizeAdd): Observable<Prize> {
    const url = `${this.urlRoot}/rounds/${roundId}/prizes`;
    return this.http.post(url, prize, this.httpOptions).pipe(
      tap((newPrize: Prize) => this.log(`created round id=${newPrize.id}`))
    );
  }

  // 呼叫 messageService
  private log(message: string): void {
    this.messageService.add(`PrizeService: ${message}`);
  }

}
