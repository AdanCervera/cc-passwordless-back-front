import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError, finalize, of } from 'rxjs';
import { Company } from 'src/app/models/company.model';
import { ContractsService } from 'src/app/services/contracts/contracts.service';

@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.css']
})
export class ContractsComponent implements OnInit {
  dataCompany: Company[] = [];  
  dataSource = new MatTableDataSource<Company>(this.dataCompany);
  displayedColumns: string[] = ['Id', 'Name', 'Address', 'Budget'];

  constructor(private contractsService: ContractsService, private router: Router) {}

  ngOnInit(): void {
    this.contractsService.getContractors()
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            this.router.navigate(['/']);
            localStorage.removeItem('token');
          }
          return of(null);
        }),
        finalize(() => {
        })
      )
      .subscribe((sub) => {
        if (sub) {
          this.dataCompany = sub;
          this.dataSource = new MatTableDataSource<Company>(this.dataCompany);
        }
      });
  }
}
