import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoginService } from './login.service';
import { HttpService } from '../http/http.service';
import { AuthenticationResponse } from 'src/app/models/authentication-response.model';
import { Login } from 'src/app/models/login.model';

describe('LoginService', () => {
  let service: LoginService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [LoginService, HttpService],
    });

    service = TestBed.inject(LoginService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });


  it('should send a GET request to GetProfile', () => {
    const testData: AuthenticationResponse = { data:true, error: false, errorMessage: '' };

    service.GetProfile().subscribe((data) => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('https://localhost:7105/api/Authentication/get-profile');
    expect(req.request.method).toBe('GET');
    req.flush(testData);
  });
});
