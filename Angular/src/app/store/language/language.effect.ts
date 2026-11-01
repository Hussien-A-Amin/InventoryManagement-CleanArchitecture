import { Actions, createEffect, ofType } from "@ngrx/effects";
import { setToArabic, setToEnglish } from "./language.actions";
import { tap } from "rxjs";
import { Injectable } from "@angular/core";

export class LanguageEffect{
@Injectable()
setToEnglishEffect=createEffect(()=> this.actions.pipe(
    ofType(setToEnglish),tap(()=>{
        localStorage.setItem('language',"en")
    }

    )
))
setToArabicEffect=createEffect(()=> this.actions.pipe(
    ofType(setToArabic),tap(()=>{
        localStorage.setItem('language',"ar")
    }

    )
))

    constructor(private actions:Actions){

    }
}