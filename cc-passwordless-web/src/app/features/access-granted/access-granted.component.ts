import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-access-granted',
  templateUrl: './access-granted.component.html',
  styleUrls: ['./access-granted.component.css']
})
export class AccessGrantedComponent implements OnInit {

  constructor(private route: Router){

  }
  ngOnInit(): void {
    setTimeout(() => {
      this.route.navigate(['/contracts'])
  }, 4000);
    
  }
}
