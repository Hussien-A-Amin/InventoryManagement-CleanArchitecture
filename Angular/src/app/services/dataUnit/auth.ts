import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IhttpResponse } from '../../models/ihttp-response';
import { IAuthResponse, IProblemDetails } from '../../models/iproduct';
import { environment } from '../../../environments/environment';
import { firstValueFrom, Observable } from 'rxjs';
import { DataUnit } from './data-unit';

@Injectable({
    providedIn: 'root',
})
export class Auth {

    constructor(private http: HttpClient, private dataUnit: DataUnit) {

    }

    
    async loginAsync(email: string, password: string): Promise<IAuthResponse> {
        try {
            const response = await firstValueFrom(
                this.http.post<IhttpResponse<IAuthResponse>>(
                    `${environment.baseUrl}/auth/login`,
                    { email, password },
                    {
                        headers: new HttpHeaders({
                            "Accept": "application/json",
                            "Content-Type": "application/json",
                        })
                    }
                )
            );

            return response.data;
        } catch (error) {
            this.dataUnit.handleError(error);
            throw error;
        }
    }
}
