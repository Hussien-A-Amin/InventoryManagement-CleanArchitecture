import { createAction, props } from "@ngrx/store";

export const setAsideStatus=createAction("setAsideStatus",props<{value:string}>());