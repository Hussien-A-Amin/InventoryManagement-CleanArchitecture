import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
logged:boolean;

constructor(private authService:AuthService){
this.logged=authService.isLogged();
}




  login(){
    this.authService.login();
this.logged=true;
  }
  logout(){
    this.authService.logOUT();
this.logged=false;
  }
  
  
}
