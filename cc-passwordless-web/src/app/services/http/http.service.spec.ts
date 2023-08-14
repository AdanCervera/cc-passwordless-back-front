import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpService } from './http.service';

describe('HttpService', () => {
  let service: HttpService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HttpService],
    });

    service = TestBed.inject(HttpService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send a GET request', () => {
    const testData = { id: 1, name: 'Test' };

    service.get('test').subscribe((data) => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('https://localhost:7105/api/test'); // Use the correct URL
    expect(req.request.method).toBe('GET');
    req.flush(testData);
  });

 
  
});
