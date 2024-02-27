import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BaseResponse } from '../models/response/base_response.model';
import { Card } from '../models/card.model';

@Injectable({
  providedIn: 'root',
})
export class BoardService {
  private URL_API: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  board(): Observable<BaseResponse> {
    return this.http.get<BaseResponse>(`${this.URL_API}/todoItem/board`);
  }

  addItem(card: Card): Observable<BaseResponse> {
    return this.http.post<BaseResponse>(`${this.URL_API}/todoItem`, card);
  }

  updateItem(card: Card): Observable<BaseResponse> {
    return this.http.patch<BaseResponse>(`${this.URL_API}/todoItem`, card);
  }
}
