import { AttendeeAdd } from '../../models/attendee/attendee-add.model';
import { Attendee } from '../../models/attendee/attendee.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AttendeeService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAllAttendeesForItemId(itemId: string): Observable<Attendee[]> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/all`;
    return this.http.get<Attendee[]>(url, this.httpOptions);
  }

  getAttendeesForItemId(itemId: string, pageIndex: number, pageSize: number): Observable<Attendee[]> {
    const url = `${this.urlRoot}/items/${itemId}/attendees?pageNumber=${pageIndex}&pageSize=${pageSize}`;
    return this.http.get<Attendee[]>(url, this.httpOptions);
  }

  getAttendeeByIdForItemId(itemId: string, attendeeId: string): Observable<Attendee> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/${attendeeId}`;
    return this.http.get<Attendee>(url, this.httpOptions);
  }

  createAttendeeForItemId(itemId: string, attendee: AttendeeAdd): Observable<Attendee> {
    const url = `${this.urlRoot}/items/${itemId}/attendees`;
    return this.http.post(url, attendee, this.httpOptions).pipe(
      map((newAttendee: Attendee) => newAttendee)
    );
  }

  getAllAttendeesLengthForItemId(itemId: string): Observable<number> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/length`;
    return this.http.get<number>(url, this.httpOptions);
  }

  getAttendeesXlsxForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/file/xlsx`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getAttendeesCsvForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/file/csv`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getAttendeesJsonForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/attendees/file/json`;
    return this.http.get(url, { responseType: 'blob' });
  }

}
