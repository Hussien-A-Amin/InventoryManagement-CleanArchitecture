import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-portal-outlet',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './portal-outlet.html',
  styleUrl: './portal-outlet.css',
})
export class PortalOutlet {}
