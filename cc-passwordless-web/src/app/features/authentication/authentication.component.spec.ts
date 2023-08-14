import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatButtonModule } from '@angular/material/button';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthenticationComponent } from './authentication.component';
import { LoginService } from 'src/app/services/login/login.service';
import { AuthenticationResponse } from 'src/app/models/authentication-response.model';

describe('AuthenticationComponent', () => {
  let component: AuthenticationComponent;
  let fixture: ComponentFixture<AuthenticationComponent>;
  let router: jasmine.SpyObj<Router>;
  let loginService: jasmine.SpyObj<LoginService>;

  beforeEach(() => {
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    const loginServiceSpy = jasmine.createSpyObj('LoginService', ['GetProfile']);

    TestBed.configureTestingModule({
      declarations: [AuthenticationComponent],
      imports: [MatProgressBarModule, MatButtonModule],
      providers: [
        { provide: Router, useValue: routerSpy },
        { provide: LoginService, useValue: loginServiceSpy },
        {
          provide: ActivatedRoute,
          useValue: { queryParams: of({ token: 'test-token' }) },
        },
      ],
    });

    fixture = TestBed.createComponent(AuthenticationComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    loginService = TestBed.inject(LoginService) as jasmine.SpyObj<LoginService>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should call GetProfile and navigate on successful request', fakeAsync(() => {
    const mockObject :AuthenticationResponse = {data: true, error: false, errorMessage:''}
    loginService.GetProfile.and.returnValue(of(mockObject));

    fixture.detectChanges();
    tick();

    expect(router.navigate).toHaveBeenCalledWith(['/access-granted']);
  }));

  it('should update message and show "Request another token" button on unauthorized error', fakeAsync(() => {
    const errorResponse: HttpErrorResponse = new HttpErrorResponse({ status: 401 });
    loginService.GetProfile.and.returnValue(throwError(errorResponse));

    fixture.detectChanges();
    tick();

    const messageElement = fixture.debugElement.query(By.css('label')).nativeElement;
    expect(messageElement.textContent).toContain('Unauthorized. Please generate another token.');

    const buttonElement = fixture.debugElement.query(By.css('.button-token')).nativeElement;
    expect(buttonElement.textContent).toContain('Request another token');
  }));

  it('should update message on generic error', fakeAsync(() => {
    const errorResponse: HttpErrorResponse = new HttpErrorResponse({ status: 500 });
    loginService.GetProfile.and.returnValue(throwError(errorResponse));

    fixture.detectChanges();
    tick();

    const messageElement = fixture.debugElement.query(By.css('label')).nativeElement;
    expect(messageElement.textContent).toContain('An error occurred while attempting to log in. Please generate another token.');
  }));


});
