import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth-service';

export const authGuard: CanActivateFn = (route, state) => {
 let _authService= inject(AuthService);
 let _router= inject(Router);


  return _authService.isLogged()?true:_router.navigateByUrl("/login");
};
