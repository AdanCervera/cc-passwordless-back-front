import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ContractsService } from './contracts.service';
import { HttpService } from '../http/http.service';
import { Company } from 'src/app/models/company.model';

describe('ContractsService', () => {
  let service: ContractsService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ContractsService, HttpService],
    });

    service = TestBed.inject(ContractsService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should getContractors return data', () => {
    const testData: Company[] = [
      { Id: 1, Name: 'Company 1', Address: 'Address 1', Budget: 1000 },
      { Id: 2, Name: 'Company 2', Address: 'Address 2', Budget: 2000 },
    ];

    service.getContractors().subscribe((data) => {
      expect(data).toEqual(testData);
    });

    const req = httpMock.expectOne('https://localhost:7105/api/Contractors'); 
    expect(req.request.method).toBe('GET');
    req.flush(testData);
  });
});
