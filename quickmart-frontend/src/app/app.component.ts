import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <app-navbar></app-navbar>
    <p-toast></p-toast>
    <p-confirmDialog></p-confirmDialog>
    <router-outlet></router-outlet>
  `,
  styles: []
})
export class AppComponent {
  title = 'QuickMart';
}
