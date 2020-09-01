import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Round } from 'src/app/models/round/round.model';
import { RoundAdd } from 'src/app/models/round/round-add.model';

@Injectable({
  providedIn: 'root'
})
export class RaffleService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getRounds(): Observable<Round[]> {
    const url = `${this.urlRoot}/rounds`;
    return this.http.get<Round[]>(url, this.httpOptions);
  }

  getRoundById(id: string): Observable<Round> {
    const url = `${this.urlRoot}/rounds/${id}`;
    return this.http.get<Round>(url, this.httpOptions);
  }

  createRound(round: RoundAdd): Observable<Round> {
    const url = `${this.urlRoot}/rounds`;
    return this.http.post(url, round, this.httpOptions).pipe(
      map((newRound: Round) => newRound)
    );
  }

}
