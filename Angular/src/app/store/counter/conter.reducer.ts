import { createReducer, on } from '@ngrx/store';
import * as CounterActions from './counter.action';

export interface CounterState {
  count: number;
}

export const initialState: CounterState = {
  count: 50,
};

export const counterReducer = createReducer(
  initialState,
  on(CounterActions.increment, (state) => ({ ...state, count: state.count + 1 })),
  on(CounterActions.decrement, (state) => ({ ...state, count: state.count - 1 })),
  on(CounterActions.reset, (state) => ({ ...state, count: 0 })),
  on(CounterActions.set, (state, { value }) => ({ ...state, count: value }))
);