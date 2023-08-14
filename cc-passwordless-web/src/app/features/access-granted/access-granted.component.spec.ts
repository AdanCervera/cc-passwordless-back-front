import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { AccessGrantedComponent } from './access-granted.component';
import { Router } from '@angular/router';

describe('AccessGrantedComponent', () => {
  let component: AccessGrantedComponent;
  let fixture: ComponentFixture<AccessGrantedComponent>;
  let router: Router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AccessGrantedComponent],
      providers: [Router], 
    });

    fixture = TestBed.createComponent(AccessGrantedComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router); 
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to /contracts after 4 seconds', fakeAsync(() => {
    const routerSpy = spyOn(router, 'navigate');

    component.ngOnInit();
    tick(4000);

    expect(routerSpy).toHaveBeenCalledWith(['/contracts']);
  }));
});
