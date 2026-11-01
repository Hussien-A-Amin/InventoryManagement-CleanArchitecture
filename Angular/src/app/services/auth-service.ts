import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IAuthResponse } from '../models/iproduct';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
 private loginSubject:BehaviorSubject<boolean>;

constructor(){
  this.loginSubject=new BehaviorSubject<boolean>(false);
}

  isLogged(): boolean {
let item= localStorage.getItem("islogged")

return item==="true";
}


login(data: IAuthResponse){





  
  localStorage.setItem("token",data.token)
  localStorage.setItem("refreshToken",data.refreshToken)
  localStorage.setItem("islogged",'true')
  this.loginSubject.next(true);
}

logOUT(){
  localStorage.removeItem("token")
  localStorage.removeItem("refreshToken")
  localStorage.removeItem("islogged")
  this.loginSubject.next(false);

}
 
getAuthSubject() :BehaviorSubject<boolean>{
 return this.loginSubject;
}

}
