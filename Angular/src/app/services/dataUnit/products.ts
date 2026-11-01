import { Injectable } from '@angular/core';
import { DataUnit } from './data-unit';
import { Iproduct } from '../../models/iproduct';
import { IhttpResponse } from '../../models/ihttp-response';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root',
})
export class Products {
     constructor(private dataUnit: DataUnit) {

    }
// async GetProductsAsync(): Promise<Iproduct[]> {
//     //     try {
//     //         const response = await firstValueFrom(
//     //             this.dataUnit.http.post<IhttpResponse<Iproduct[]>>(
//     //                 `${environment.baseUrl}/auth/login`,
                   
//     //             )
//     //         );

//     //         return response.data;
//     //     } catch (error) {
//     //         this.dataUnit.handleError(error);
//     //         throw error;
//     //     }
//     // }
// }










}