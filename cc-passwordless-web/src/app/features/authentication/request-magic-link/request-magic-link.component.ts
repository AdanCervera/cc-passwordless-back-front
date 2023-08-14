import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { catchError, finalize, of } from 'rxjs';
import { Login } from 'src/app/models/login.model';
import { HttpService } from 'src/app/services/http/http.service';
import { LoginService } from 'src/app/services/login/login.service';

@Component({
  selector: 'app-request-magic-link',
  templateUrl: './request-magic-link.component.html',
  styleUrls: ['./request-magic-link.component.css']
})
export class RequestMagicLinkComponent implements OnInit {

  loginForm!: FormGroup;
  errorMessage!: string;
  loginModel: Login = { email : ''};
  isLoading: boolean = false;
  isSuccessfullRequest: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit(): void {
    this.errorMessage = '';
     if (this.loginForm.valid) {
      this.isLoading = true;
      this.loginModel.email = this.loginForm.get('email')?.value;
      this.loginService
      .RequestMagicLink(this.loginModel)
      .pipe(
        catchError((error) => {
          this.errorMessage = 'Error while trying to connect to the server.';
          return of(null);
        }),
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe((sub) => {
        if (sub && !sub.error) {
          this.isSuccessfullRequest = true;
        }
      });
    }
  }
}
