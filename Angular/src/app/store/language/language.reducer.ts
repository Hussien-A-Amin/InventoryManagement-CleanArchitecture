import { createReducer, on, State } from "@ngrx/store";
import { setToArabic, setToEnglish } from "./language.actions";

export const languageReducer=createReducer('en',
    on(setToEnglish,(st)=>st="en"),
    on(setToArabic,(st)=>st="ar")

)