import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateNewsFeedComponent } from './create-or-update-news-feed.component';

describe('CreateOrUpdateNewsFeedComponent', () => {
  let component: CreateOrUpdateNewsFeedComponent;
  let fixture: ComponentFixture<CreateOrUpdateNewsFeedComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateOrUpdateNewsFeedComponent]
    });
    fixture = TestBed.createComponent(CreateOrUpdateNewsFeedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
