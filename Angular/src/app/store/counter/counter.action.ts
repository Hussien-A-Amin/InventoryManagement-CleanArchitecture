import { createAction, props } from '@ngrx/store';

// Simple actions
export const increment = createAction('[Counter Component] Increment');
export const decrement = createAction('[Counter Component] Decrement');
export const reset = createAction('[Counter Component] Reset');

// Action with a payload
export const set = createAction(
  '[Counter Component] Set',
  props<{ value: number }>()
);