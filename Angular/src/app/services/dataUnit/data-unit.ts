import { Injectable } from '@angular/core';
import { Auth } from './auth';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { IProblemDetails } from '../../models/iproduct';
import { AlertCenter } from '../alert-center';

@Injectable({
    providedIn: 'root',
})
export class DataUnit {
    constructor(private alertCenter: AlertCenter, public http: HttpClient) {

                
    }



    public auth(): Auth {
        return new Auth(this.http, this);
    }





    handleError(error: unknown) {
        if (error instanceof HttpErrorResponse) {

            const problem: IProblemDetails = error.error;

            this.alertCenter.error(problem.title!, problem.detail!);
            if(problem.errors!==null){

                console.error(`Errors:`, problem.errors);
            }
        }

        else{

        }
    }
}
