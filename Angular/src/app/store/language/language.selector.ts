import { createFeatureSelector, createSelector } from "@ngrx/store";

export const selectlanguageState = createFeatureSelector<string>('language');

// Selector to get just the count number
export const selectlanguaget = createSelector(
  selectlanguageState,
  (state) => state
);