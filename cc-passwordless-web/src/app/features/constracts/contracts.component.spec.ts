import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatTableModule } from '@angular/material/table';
import { ContractsComponent } from './contracts.component';
import { Company } from 'src/app/models/company.model';
import { ContractsService } from 'src/app/services/contracts/contracts.service';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

describe('ContractsComponent', () => {
  let component: ContractsComponent;
  let fixture: ComponentFixture<ContractsComponent>;
  let contractsService: jasmine.SpyObj<ContractsService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(() => {
    const contractsServiceSpy = jasmine.createSpyObj('ContractsService', ['getContractors']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [ContractsComponent],
      imports: [MatTableModule],
      providers: [
        { provide: ContractsService, useValue: contractsServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    });

    fixture = TestBed.createComponent(ContractsComponent);
    component = fixture.componentInstance;
    contractsService = TestBed.inject(ContractsService) as jasmine.SpyObj<ContractsService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch data from service and populate dataSource on successful request', () => {
    const testData: Company[] = [
      { Id: 1, Name: 'Company 1', Address: 'Address 1', Budget: 1000 },
      { Id: 2, Name: 'Company 2', Address: 'Address 2', Budget: 2000 },
    ];

    contractsService.getContractors.and.returnValue(of(testData));

    fixture.detectChanges();

    expect(contractsService.getContractors).toHaveBeenCalled();
    expect(component.dataSource.data).toEqual(testData);
  });

  it('should navigate to home and remove token on unauthorized error', () => {
    const errorResponse: HttpErrorResponse = new HttpErrorResponse({ status: 401 });
    contractsService.getContractors.and.returnValue(throwError(errorResponse));
  
    spyOn(localStorage, 'removeItem'); 
  
    component.ngOnInit(); 
  
    expect(router.navigate).toHaveBeenCalledWith(['/']);
    expect(localStorage.removeItem).toHaveBeenCalledWith('token');
  });
  
  

});
