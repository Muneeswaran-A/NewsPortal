import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { api } from '../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private httpClient: HttpClient) { }

  getCategories(): Observable<ICategory[]> {
    const endpoint = `${api.endpoint}/api/Category`
    return this.httpClient.get<ICategory[]>(endpoint);
  }
}

export interface ICategory {
  id: number,
  name: string,
}