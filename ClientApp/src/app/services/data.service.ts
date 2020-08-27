import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { TempData } from '../models/temp-data.model';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getTempData(): Observable<TempData> {
    return this.http.get<TempData>('http:localhost:5001', this.httpOptions);
  }

}
