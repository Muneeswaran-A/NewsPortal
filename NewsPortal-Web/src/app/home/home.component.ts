import { Component, OnInit, ViewChild } from '@angular/core';
import { NewsService } from '../services/news.service';
import { faSearch, faAdd, faPencil, faTrash } from '@fortawesome/free-solid-svg-icons';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmDialogService } from '../services/confirm-dialog.service';
import { Subject, debounceTime } from 'rxjs';
import { INews } from '../definitions/inews';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  private alertSubject = new Subject<string>();
  news?: INews[];
  currentPage = 1;
  totalRecords = 0;
  pageSize = 5;
  isLoading = true;
  searchText?: string;
  faSearch = faSearch
  faAdd = faAdd
  faPencil = faPencil
  faTrash = faTrash
  alertMessage = '';
  alertMessageType = 'success';

  @ViewChild('selfClosingAlert', { static: false }) selfClosingAlert?: NgbAlert;

  constructor(private newsService: NewsService, private confirmDialogService: ConfirmDialogService) {
  }

  ngOnInit() {
    this.alertSubject.subscribe((message) => (this.alertMessage = message));
    this.alertSubject.pipe(debounceTime(5000)).subscribe(() => {
      if (this.selfClosingAlert) {
        this.selfClosingAlert.close();
      }
    });
    this.loadNewsFeeds(this.currentPage, this.pageSize);
  }

  public searchNewsFeeds() {
    this.loadNewsFeeds(this.currentPage, this.pageSize);
  }
  public loadNewsFeeds(page: number = 1, itemsPerPage: number = 5): void {
    this.isLoading = true;
    this.newsService.getNews(page, itemsPerPage, this.searchText).subscribe({
      next: (response) => {
        this.isLoading = false;
        this.news = response.items;
        this.currentPage = response.metadata.pageNo;
        this.totalRecords = response.metadata.totalRecords;
      },
      error: (error) => {
        // error handling
        this.isLoading = false;
        this.news = [];
        this.currentPage = 1;
        this.totalRecords = 0;
      }
    });
  }

  onPageChange(page: number) {
    this.currentPage = page;
    this.loadNewsFeeds(this.currentPage, this.pageSize);
  }

  onDeleteNews(newsId: number) {
    this.showDialog(newsId);
  }

  public showAlertMessage(message: string, alertType: string = "success") {
    this.alertMessageType = alertType;
    this.alertSubject.next(message);
  }

  showDialog(id: number) {
    const that = this;
    this.confirmDialogService.confirmThis("Are you sure to delete?",
      function () {
        that.newsService.deleteNews(id).subscribe({
          next: async (response) => {
            that.showAlertMessage("Deleted succesfully.", "danger");
            that.loadNewsFeeds(1, that.pageSize);
          },
          error: (error) => {
            that.showAlertMessage(`failed to delete news ${error.detail}`);
          }
        });
      },
      function () {
      })
  }

}
