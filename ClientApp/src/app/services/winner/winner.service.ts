import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Attendee } from '../../models/attendee/attendee.model';

@Injectable({
  providedIn: 'root'
})
export class WinnerService {

  urlRoot = environment.apiUrl;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getWinnersForItemId(itemId: string, pageIndex: number, pageSize: number): Observable<Attendee[]> {
    const url = `${this.urlRoot}/items/${itemId}/winners?pageNumber=${pageIndex}&pageSize=${pageSize}`;
    return this.http.get<Attendee[]>(url, this.httpOptions);
  }

  getAllWinnersLengthForItemId(itemId: string): Observable<number> {
    const url = `${this.urlRoot}/items/${itemId}/winners/length`;
    return this.http.get<number>(url, this.httpOptions);
  }

  getWinnersXlsxForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/winners/file/xlsx`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getWinnersCsvForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/winners/file/csv`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getWinnersJsonForItemId(itemId: string): Observable<Blob> {
    const url = `${this.urlRoot}/items/${itemId}/winners/file/json`;
    return this.http.get(url, { responseType: 'blob' });
  }

}
