import { Component } from '@angular/core';
import { faNewspaper, faHome, faCircleInfo } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrls: ['./app-header.component.css']
})
export class AppHeaderComponent {
  faNewspaper = faNewspaper;
  faHome = faHome;
  faInfo = faCircleInfo;
}
