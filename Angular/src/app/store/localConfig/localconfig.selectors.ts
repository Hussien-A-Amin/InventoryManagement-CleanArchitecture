import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IlocalConfig } from "./localconfig.reducer";

export const selectlocalConfigState = createFeatureSelector<IlocalConfig>('counter');

// Selector to get just the count number
export const selectlocalConfig = createSelector(
  selectlocalConfigState,
  (state) => state
);