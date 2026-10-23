import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
 private loginSubject:BehaviorSubject<boolean>;

constructor(){
  this.loginSubject=new BehaviorSubject<boolean>(false);
}

  isLogged(): boolean {
let item= localStorage.getItem("token")

return item!=null;
}


login(){
  localStorage.setItem("token","fgdsfgasfgSADFGHAFDHSDH")
  this.loginSubject.next(true);
}

logOUT(){
  localStorage.removeItem("token")
  this.loginSubject.next(false);

}
 
getAuthSubject() :BehaviorSubject<boolean>{
 return this.loginSubject;
}

}
