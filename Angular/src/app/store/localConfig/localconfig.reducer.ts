import { createReducer, on } from "@ngrx/store";
import * as localConfigActions   from "./localconfig.actions";

export interface IlocalConfig{
    asideStatus:string
}

export const initialState: IlocalConfig = {
   asideStatus:"mini"
};



export const localConfigReducer= createReducer(initialState,
    on(localConfigActions.setAsideStatus,(st)=>st={asideStatus:"fgk"})
)