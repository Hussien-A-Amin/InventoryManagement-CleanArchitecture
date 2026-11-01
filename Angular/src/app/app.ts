import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SwalWrapper } from './components/swal-wrapper/swal-wrapper';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectlanguageState } from './store/language/language.selector';
import { AsyncPipe } from '@angular/common';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet,SwalWrapper,AsyncPipe],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('ecomerce');
  lang:Observable<string>
  dir="ltr";
  constructor(private store:Store){
this.lang=store.select(selectlanguageState);
this.lang.subscribe({
  next:(dr)=>{
    this.dir=dr==="en"?"ltr":"rtl";

  }
})
  }
}
