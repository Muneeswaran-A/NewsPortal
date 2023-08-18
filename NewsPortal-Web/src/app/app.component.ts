import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <div class="d-flex h-100 text-center text-white bg-dark">
    <div class='cover-container d-flex w-100 h-100 p-3 mx-auto flex-column'>
      <app-header class="bg-dark pb-3 sticky-top"> </app-header>
      <main class="py-3">
        <router-outlet></router-outlet>
      </main>
      <app-footer class="mt-auto text-white-50"> </app-footer>
    </div>
  </div>
`
})

export class AppComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  title = "News Portal"
}
