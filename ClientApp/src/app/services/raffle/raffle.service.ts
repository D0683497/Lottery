import { ItemAdd } from '../../models/item/item-add.model';
import { Item } from '../../models/item/item.model';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RaffleService {

  urlRoot = 'http://localhost:5001/api';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAllItems(): Observable<Item[]> {
    const url = `${this.urlRoot}/items/all`;
    return this.http.get<Item[]>(url, this.httpOptions);
  }

  getItems(pageIndex: number, pageSize: number): Observable<Item[]> {
    const url = `${this.urlRoot}/items?pageNumber=${pageIndex}&pageSize=${pageSize}`;
    return this.http.get<Item[]>(url, this.httpOptions);
  }

  getItemById(itemId: string): Observable<Item> {
    const url = `${this.urlRoot}/items/${itemId}`;
    return this.http.get<Item>(url, this.httpOptions);
  }

  createItem(round: ItemAdd): Observable<Item> {
    const url = `${this.urlRoot}/items`;
    return this.http.post(url, round, this.httpOptions).pipe(
      map((newRound: Item) => newRound)
    );
  }

  getAllItemsLength(): Observable<number> {
    const url = `${this.urlRoot}/items/length`;
    return this.http.get<number>(url, this.httpOptions);
  }

}
