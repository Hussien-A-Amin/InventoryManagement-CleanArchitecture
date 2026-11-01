import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CounterState } from './conter.reducer';

// Feature selector
export const selectCounterState = createFeatureSelector<CounterState>('counter');

// Selector to get just the count number
export const selectCount = createSelector(
  selectCounterState,
  (state) => state.count
);