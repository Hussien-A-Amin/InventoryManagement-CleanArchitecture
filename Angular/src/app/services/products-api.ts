import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { INewproduct, Iproduct } from '../models/iproduct';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IhttpResponse } from '../models/ihttp-response';
import { ICategory } from '../models/i-category';

@Injectable({
  providedIn: 'root',
})
export class ProductsApi {

  constructor(private http:HttpClient){

  }

  Create(prd:INewproduct):Observable<IhttpResponse<Iproduct>>{


   return this.http.post<IhttpResponse<Iproduct>>(`${environment.baseUrl}/Products/create`,prd,{
    headers:new HttpHeaders({
      "Accept":"application/json",
      "Content-Type":"application/json",
    }),
    

   })
  
  }
  getAll():Observable<IhttpResponse<Iproduct[]>>{


   return this.http.get<IhttpResponse<Iproduct[]>>(`${environment.baseUrl}/products`)
  
  }
  getAllCategories():Observable<IhttpResponse<ICategory[]>>{


   return this.http.get<IhttpResponse<ICategory[]>>(`${environment.baseUrl}/categories`)
  
  }
  
  getById(id:string):Observable<IhttpResponse<Iproduct>>{
   return this.http.get<IhttpResponse<Iproduct>>(`${environment.baseUrl}/products/${id}`)
  }

  getByCatId(id:string):Observable<IhttpResponse<Iproduct[]>>{
   return this.http.get<IhttpResponse<Iproduct[]>>(`${environment.baseUrl}/products/byCategoryId/${id}`)
  }

  delete(id:string):Observable<boolean>{
   return this.http.delete<boolean>(`${environment.baseUrl}/products/${id}`)
  }





}
