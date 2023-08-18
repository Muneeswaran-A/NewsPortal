import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { INewsResponse } from '../definitions/INewsResponse';
import { INews } from '../definitions/inews';
import { News } from '../definitions/News';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  private url = 'https://localhost:7251';

  constructor(private httpClient: HttpClient) { }

  getNews(pageNo: number = 1, pageSize: number = 5, searchText: string = ""): Observable<INewsResponse> {
    const endpoint = `${this.url}/api/NewsFeed/GetWithFilter?SearchText=${searchText}&PageNo=${pageNo}&PageSize=${pageSize}`
    return this.httpClient.get<INewsResponse>(endpoint);
  }

  getNewsById(id: string) {
    const endpoint = `${this.url}/api/NewsFeed/Get/${id}`
    return this.httpClient.get<INews>(endpoint);
  }

  createNews(news: News) {
    const endpoint = `${this.url}/api/NewsFeed/Create`
    return this.httpClient.post<any>(endpoint, news);
  }

  updateNews(news: News) {
    const endpoint = `${this.url}/api/NewsFeed/Update/${news.id}`
    return this.httpClient.put<any>(endpoint, news);
  }

  deleteNews(id: number) {
    const endpoint = `${this.url}/api/NewsFeed/Delete/${id}`
    return this.httpClient.delete<any>(endpoint);
  }
}

