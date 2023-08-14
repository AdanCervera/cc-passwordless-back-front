import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RequestMagicLinkComponent } from './request-magic-link.component';
import { LoginService } from 'src/app/services/login/login.service';
import { of, throwError } from 'rxjs';
import { AuthenticationResponse } from 'src/app/models/authentication-response.model';

describe('RequestMagicLinkComponent', () => {
  let component: RequestMagicLinkComponent;
  let fixture: ComponentFixture<RequestMagicLinkComponent>;
  let loginService: jasmine.SpyObj<LoginService>;
  let formBuilder: FormBuilder; 
  beforeEach(() => {
    const loginServiceSpy = jasmine.createSpyObj('LoginService', ['RequestMagicLink']);

    TestBed.configureTestingModule({
      declarations: [RequestMagicLinkComponent],
      imports: [
        ReactiveFormsModule,
        MatCardModule,
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
      ],
      providers: [{ provide: LoginService, useValue: loginServiceSpy, FormBuilder }],
    });

    fixture = TestBed.createComponent(RequestMagicLinkComponent);
    component = fixture.componentInstance;
    loginService = TestBed.inject(LoginService) as jasmine.SpyObj<LoginService>;
    formBuilder = TestBed.inject(FormBuilder); 
    component.loginForm = formBuilder.group({
      email: ['test@example.com'],
    });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set errorMessage and isLoading on server error', fakeAsync(() => {
    const errorResponse = { error: 'Server error' };
    loginService.RequestMagicLink.and.returnValue(throwError(errorResponse));

    component.onSubmit();
    tick();

    expect(component.errorMessage).toBe('Error while trying to connect to the server.');
    expect(component.isLoading).toBeFalse();
    expect(component.isSuccessfullRequest).toBeFalse();
  }));

  it('should set isSuccessfullRequest on successful request', fakeAsync(() => {
    const successResponse : AuthenticationResponse = { data: true, error: false, errorMessage:''};
    loginService.RequestMagicLink.and.returnValue(of(successResponse));

    component.onSubmit();
    tick();

    expect(component.errorMessage).toBe('');
    expect(component.isLoading).toBeFalse();
    expect(component.isSuccessfullRequest).toBeTrue();
  }));
});
