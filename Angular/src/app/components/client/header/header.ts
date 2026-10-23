import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth-service';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header implements OnInit {

  
  logged:boolean;

constructor(private authService:AuthService){
this.logged=authService.isLogged();
}
  ngOnInit(): void {
this.authService.getAuthSubject().subscribe({
  next:(bool)=>{this.logged=bool}
});
}





}
