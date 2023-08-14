import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, CanLoad, Route, Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
  })
export class AuthGuard {

    constructor(private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const token = localStorage.getItem('token');
    if (token) {
      return true;
    } else {
      this.router.navigate(['/']); 
      return false;
    }
  }
}

export const AuthenticationGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
    return inject(AuthGuard).canActivate(next, state);
  }
