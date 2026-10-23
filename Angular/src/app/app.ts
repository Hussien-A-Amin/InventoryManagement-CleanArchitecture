import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SwalWrapper } from './components/swal-wrapper/swal-wrapper';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet,SwalWrapper],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ecomerce');
}
