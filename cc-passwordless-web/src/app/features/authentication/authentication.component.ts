import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProgressBarMode } from '@angular/material/progress-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, finalize, of } from 'rxjs';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.css']
})
export class AuthenticationComponent implements OnInit {

  message: string = "Kindly wait while we retrieve your information."
  mode: ProgressBarMode = 'indeterminate';
  error: boolean = false;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService){
    
  }


ngOnInit(): void {
  this.route.queryParams.subscribe(params => {
    const token = params['token'];
    if (token) {
      localStorage.setItem('token', token);
      this.loginService.GetProfile()
        .pipe(
          catchError((error: HttpErrorResponse) => {
            if (error.status === 401) {
              this.message = 'Unauthorized. Please generate another token.';
              localStorage.removeItem('token');
            } else {
              this.message = 'An error occurred while attempting to log in. Please generate another token.';
            }
            this.error = true;
            return of(null);
          }),
          finalize(() => {
            this.mode ='determinate';
          })
        )
        .subscribe((sub) => {
          if (sub !== null) {
            this.router.navigate(['/access-granted']); 
          }
        });
    }
  });
}

  

  goToLogin(): void {
    this.router.navigate(['/']);
  }
}
