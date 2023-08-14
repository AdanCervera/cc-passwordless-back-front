import { Injectable } from '@angular/core';
import { HttpService } from '../http/http.service';
import { Observable } from 'rxjs';
import { Company } from 'src/app/models/company.model';

@Injectable({
  providedIn: 'root'
})
export class ContractsService {
  constructor(private httpService: HttpService) { }

  public getContractors(): Observable<Company[]>{
    return this.httpService.get('Contractors');
  }

}
