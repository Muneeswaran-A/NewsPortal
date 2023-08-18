import { Component, OnInit, ViewChild } from '@angular/core';
import { CategoryService, ICategory } from '../services/category.service';
import { ActivatedRoute, Router } from '@angular/router'
import { faL, faSave } from '@fortawesome/free-solid-svg-icons';
import { NewsService } from '../services/news.service';
import { News } from '../definitions/News';
import { NgbAlert } from '@ng-bootstrap/ng-bootstrap';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-create-or-update-news-feed',
  templateUrl: './create-or-update-news-feed.component.html',
  styleUrls: ['./create-or-update-news-feed.component.css']
})
export class CreateOrUpdateNewsFeedComponent implements OnInit {
  private alertSubject = new Subject<string>();
  id?: string | null;
  model = new News();
  pageTitle: string = "Create news feed";
  submitButtonText: string = "Create";
  categories?: ICategory[];
  selectedCategory?: number;
  faSave = faSave;
  alertMessage = '';
  alertMessageType = 'success';
  isLoading = true;

  @ViewChild('selfClosingAlert', { static: false }) selfClosingAlert?: NgbAlert;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService,
    private newsService: NewsService) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    this.alertSubject.subscribe((message) => (this.alertMessage = message));
    this.alertSubject.pipe(debounceTime(5000)).subscribe(() => {
      if (this.selfClosingAlert) {
        this.selfClosingAlert.close();
      }
    });
    this.id = this.route.snapshot.paramMap.get('id');
    this.categoryService.getCategories().subscribe({
      next: (response) => {
        this.isLoading = false;
        this.categories = response;
      },
      error: (error) => {
        // error handling
        this.isLoading = false;
      }
    });
    if (this.id != null) {
      this.pageTitle = "Edit news feed";
      this.submitButtonText = "Save";
      this.isLoading = true;
      this.newsService.getNewsById(this.id).subscribe({
        next: (response) => {
          this.model = response;
          this.isLoading = false;
        },
        error: (error) => {
          this.isLoading = false;
        }
      });
    }
  }

  onSubmit(form: any) {
    if (form.invalid) {
      return;
    }
    this.isLoading = true;
    console.info("Form Valid!")
    if (this.id == null) {
      this.newsService.createNews(this.model).subscribe({
        next: async (response) => {
          this.showAlertMessage("Created succesfully, Redirecting to home page.");
          this.isLoading = false;
          for (let i = 3; i > 0; i--) {
            this.showAlertMessage(`Created succesfully, Redirecting to home page in ${i} seconds.`);
            await new Promise(f => setTimeout(f, 1000));;
          }
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.isLoading = false;
        }
      });
    }
    else {
      this.newsService.updateNews(this.model).subscribe({
        next: async (response) => {
          this.showAlertMessage("Updated succesfully, Redirecting to home page.");
          this.isLoading = false;
          for (let i = 3; i > 0; i--) {
            this.showAlertMessage(`Updated succesfully, Redirecting to home page in ${i} seconds.`);
            await new Promise(f => setTimeout(f, 1000));;
          }
          this.router.navigate(['/home']);
        },
        error: (error) => {
          this.isLoading = false;
        }
      });
    }
  }

  public showAlertMessage(message: string, alertType: string = "success") {
    this.alertMessageType = alertType;
    this.alertSubject.next(message);
  }

}
