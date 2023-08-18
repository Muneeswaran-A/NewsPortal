import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private url = 'https://localhost:7251';

  constructor(private httpClient: HttpClient) { }

  getCategories(): Observable<ICategory[]> {
    const endpoint = `${this.url}/api/Category`
    return this.httpClient.get<ICategory[]>(endpoint);
  }
}

export interface ICategory {
  id: number,
  name: string,
}