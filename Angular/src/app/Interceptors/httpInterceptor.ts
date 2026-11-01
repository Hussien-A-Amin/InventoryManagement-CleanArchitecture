import { HttpErrorResponse, HttpEvent, HttpEventType, HttpHandlerFn, HttpHeaders, HttpParams, HttpRequest, HttpResponse } from "@angular/common/http";
import { inject } from "@angular/core/primitives/di";
import { Router } from "@angular/router";
import { catchError, tap, throwError } from "rxjs";

export function httpInterceptor(req: HttpRequest<any>, next: HttpHandlerFn) {
const router=inject(Router);
    const rq = req.clone({
        headers: req.headers.append("kaf", "kaaaaaaaaaaaaaaaaaaaaaaaaf")
    })


    




return next(rq).pipe(
    tap((event)=>{
        
        if(event.type==HttpEventType.Response){
            if(event.status==200){

            }
            else{
            }
        }
        else {
            console.error("Incomplete Proccess")
            throw Error();
        }
    })
)
// return next(rq).pipe(
//     catchError((error) => {
//             const errorPayload = error.error; // This will capture your Problem Details object

//             switch (error.status) {
//                 case 401:
//                     // Token expired or invalid: Clear storage and redirect to login
//                     localStorage.removeItem('auth_token');
//                     router.navigate(['/login']);
//                     break;

//                 case 403:
//                     console.warn('Access denied. You do not have permission to view this resource.');
//                     // alertCenter.show("You do not have permission to perform this action.");
//                     break;

//                 case 404:
//                     console.warn('The requested resource was not found:', error.url);
//                     break;

//                 case 500:
//                 case 502:
//                 case 503:
//                     console.error('A server-side error occurred. Please try again later.');
//                     // alertCenter.show("Server error. Please try again later.");
//                     break;

//                 default:
//                     // Handles other status codes (like 400 Bad Request with Problem Details)
//                     break;
//             }}));

           




}