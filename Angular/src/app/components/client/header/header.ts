import { Component, EventEmitter, input, OnInit, output, Output } from '@angular/core';
import { AuthService } from '../../../services/auth-service';
import { setToArabic, setToEnglish } from '../../../store/language/language.actions';
import { Store } from '@ngrx/store';
import { selectlanguaget } from '../../../store/language/language.selector';
import { IlocalConfig } from '../../../store/localConfig/localconfig.reducer';
import { selectlocalConfig } from '../../../store/localConfig/localconfig.selectors';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {

@Output() 
offCanvasClicked= new EventEmitter<string>();


  logged: boolean;
  isArabic: boolean = false;

  constructor(private authService: AuthService, private store: Store) {
    this.logged = authService.isLogged();





    this.store.select(selectlanguaget).subscribe({
      next: (lang) => {
        this.isArabic = lang == "ar";

      }
    })
  }
  ngOnInit(): void {
    this.authService.getAuthSubject().subscribe({
      next: (bool) => { this.logged = bool }
    });
  }
changeOfCanvaStatus() {
this.offCanvasClicked.emit()
}





  setArabic() {
    this.store.dispatch(setToArabic());

  }
  setEnglish() {
    this.store.dispatch(setToEnglish());

  }



}
