import { Component } from '@angular/core';
import { State, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { AsyncPipe } from '@angular/common';
import * as CounterActions from '../../../store/counter/counter.action';
import { selectCount } from '../../../store/counter/counter.selector';
import { setToArabic } from '../../../store/language/language.actions';

@Component({
  selector: 'app-footer',
  imports: [AsyncPipe],
  templateUrl: './footer.html',
  styleUrl: './footer.css',
})
export class Footer {
count$: Observable<number>;

  constructor(private store: Store) {
    this.count$ = this.store.select(selectCount);
  }

  increment() {
    this.store.dispatch(CounterActions.increment());
  }

  decrement() {
    this.store.dispatch(CounterActions.decrement());
  }

  reset() {
    this.store.dispatch(CounterActions.reset());
  }

  setToTen() {
    this.store.dispatch(CounterActions.set({ value: 10 }));
  }
 
}
