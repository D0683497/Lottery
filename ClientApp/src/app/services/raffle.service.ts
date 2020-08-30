import { catchError, map, tap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { RoundAdd } from './../models/round/round-add.model';
import { Round } from '../models/round/round.model';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class RaffleService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private messageService: MessageService, private http: HttpClient) { }

  getRounds(): Observable<Round[]> {
    const url = `${this.urlRoot}/rounds`;
    return this.http.get<Round[]>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log('fetched rounds')),
        // catchError(this.handleError<Round[]>('getRounds', []))
      );
  }

  getRoundById(id: string): Observable<Round> {
    const url = `${this.urlRoot}/rounds/${id}`;
    return this.http.get<Round>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log(`fetched round id=${id}`)),
        // catchError(this.handleError<Round>(`getRoundById id=${id}`))
      );
  }

  createRound(round: RoundAdd): Observable<Round> {
    const url = `${this.urlRoot}/rounds`;
    return this.http.post(url, round, this.httpOptions).pipe(
      tap((newRound: Round) => this.log(`created round id=${newRound.id}`)),
      // catchError(this.handleError<Round>('createRound'))
    );
  }

  deleteRound(id: string): Observable<any> {
    const url = `${this.urlRoot}/rounds/${id}`;
    return this.http.delete(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleted round id=${id}`))
    );
  }

  // 呼叫 messageService
  private log(message: string): void {
    this.messageService.add(`RaffleService: ${message}`);
  }

  // 處理錯誤
  private handleError<T>(operation = 'operation', result?: T): any {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }

}

// https://angular.tw/tutorial/toh-pt4

// https://angular.tw/tutorial/toh-pt6
