import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { DataUnit } from '../../services/dataUnit/data-unit';
import { FormsModule } from '@angular/forms';
import { AlertCenter } from '../../services/alert-center';

@Component({
  selector: 'app-login',
  imports: [RouterLink,FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  logged: boolean;
  email: string = "";
  password: string = "";

  constructor(private authService: AuthService,private router:Router,private alertCenter: AlertCenter, private dataUnit: DataUnit) {
    this.logged = authService.isLogged();
  }




 async login() {


  var d= await this.dataUnit.auth().loginAsync(this.email, this.password);
          this.authService.login(d);

          this.alertCenter.success("Logged","");
          this.router.navigateByUrl("/client/overview");
  
    // this.dataUnit.auth().login(this.email, this.password).subscribe({
    //   next: (response) => {
    //     if (response.isSuccess) {
    
    //       this.authService.login(response.data);

    //     }

    //   },error:(err)=>{


    //     this.alertCenter.handleError(err);

        
    //   }
    // });
  }
  logout() {
    this.authService.logOUT();

  }


}
