import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, RouterLinkActive, RouterLink } from '@angular/router';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';

@Component({
  selector: 'app-client-outlet',
  imports: [RouterLink, RouterLinkActive, Footer, Header, RouterOutlet],
  templateUrl: './client-outlet.html',
  styleUrl: './client-outlet.css',
})
export class ClientOutlet {}
