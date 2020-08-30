import { AttendeeAdd } from '../models/attendee/attendee-add.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { MessageService } from './message.service';
import { Attendee } from '../models/attendee/attendee.model';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AttendeeService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private messageService: MessageService) { }

  createAttendee(roundId: string, attendee: AttendeeAdd): Observable<Attendee> {
    const url = `${this.urlRoot}/rounds/${roundId}/attendees`;
    return this.http.post(url, attendee, this.httpOptions).pipe(
      tap((newAttendee: Attendee) => this.log(`created attendee id=${newAttendee.id}`))
    );
  }

  // 呼叫 messageService
  private log(message: string): void {
    this.messageService.add(`AttendeeService: ${message}`);
  }

}
