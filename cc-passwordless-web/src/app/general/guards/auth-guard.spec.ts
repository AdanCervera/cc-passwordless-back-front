import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthGuard, AuthenticationGuard } from './auth-guard';


describe('AuthGuard', () => {
  let guard: AuthGuard;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
    });
    guard = TestBed.inject(AuthGuard);
    router = TestBed.inject(Router);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should return true if token exists', () => {
    spyOn(localStorage, 'getItem').and.returnValue('test-token');

    const canActivate = guard.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    expect(canActivate).toBeTrue();
  });

  it('should navigate to home and return false if token does not exist', () => {
    spyOn(localStorage, 'getItem').and.returnValue(null);
    const navigateSpy = spyOn(router, 'navigate');

    const canActivate = guard.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    expect(canActivate).toBeFalse();
    expect(navigateSpy).toHaveBeenCalledWith(['/']);
  });
});

describe('AuthenticationGuard', () => {
  it('should call canActivate method of AuthGuard', () => {
    const authGuard = jasmine.createSpyObj('AuthGuard', ['canActivate']);
    const next = {} as ActivatedRouteSnapshot;
    const state = {} as RouterStateSnapshot;

    AuthenticationGuard(next, state);

    expect(authGuard.canActivate).toHaveBeenCalledWith(next, state);
  });
});
