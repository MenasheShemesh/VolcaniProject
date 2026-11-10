import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IMessage } from '../models/IMessage';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpClient: HttpClient) { }

  public getMessagesByStatus(status: number): Observable<IMessage[]> {
    return this.httpClient.get<IMessage[]>('messages', { params: { status: status } });
  }

  public addMessage(newMessage: IMessage): Observable<HttpResponse<Object>> {
    return this.httpClient.post('messages', newMessage, { observe: 'response' });
  }

  public getAllMessageStatuses(): Observable<Record<number, string>> {
    return this.httpClient.get<Record<number, string>>('messages/statuses');
  }
}
