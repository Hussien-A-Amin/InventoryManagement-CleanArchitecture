import { Component } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, RouterLinkActive, RouterLink } from '@angular/router';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';
import { Store } from '@ngrx/store';
import { selectlanguaget } from '../../../store/language/language.selector';
import { selectlocalConfig } from '../../../store/localConfig/localconfig.selectors';
import { setAsideStatus } from '../../../store/localConfig/localconfig.actions';

@Component({
  selector: 'app-client-outlet',
  imports: [RouterLink, RouterLinkActive, Footer, Header, RouterOutlet],
  templateUrl: './client-outlet.html',
  styleUrl: './client-outlet.css',
})
export class ClientOutlet {



  showSideBar() {

    this.addclass("offcanvas-active")
    this.asideClass = "show";
    this.offcanvasClass = "show";

  }

  
  hideSidebar() {
    this.asideClass = "";
    this.offcanvasClass = "";

  }
  addclass(css: string) {
    this.outLetClasses = `${this.outLetClasses} ${css}`.trim();
  }
  removeclass(css: string) {
    this.outLetClasses = this.outLetClasses.replace(css, "").trim();

  }



  addOrRemoveclass(css: string): Boolean {
    if (this.outLetClasses.includes(css)) {
      this.outLetClasses = this.outLetClasses.replace(css, "").trim();
    }
    else {
      this.outLetClasses = `${this.outLetClasses} ${css}`.trim();
    }
    return true;
  }

  changeSideStatus() {
    this.addOrRemoveclass("aside-mini");
  }

  mainClass = "main-wrap";
  outLetClasses = "";
  asideClass = "";
  offcanvasClass = "";
  constructor(private store: Store) {

    this.store.select(selectlocalConfig).subscribe({
      next: (config) => {
        let v = config.asideStatus == "mini" ? "aside-mini" : "";
        this.addOrRemoveclass("aside-mini");


      }
    })


    this.store.select(selectlanguaget).subscribe({
      next: (lang) => {

        if (lang === "en")
          this.mainClass = "main-wrap";
        else
          this.mainClass = "main-wrap-right";

      }
    })
  }
}
