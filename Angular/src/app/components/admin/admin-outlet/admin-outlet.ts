import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-admin-outlet',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './admin-outlet.html',
  styleUrl: './admin-outlet.css',
})
export class AdminOutlet {}
