import { Component, OnInit } from '@angular/core';
import { ApiconstantService } from 'src/app/helpers/httpConstant';
import { HttpmoduleService } from 'src/app/helpers/httpmodule.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  result:any;
  constructor(private httpHelper:HttpmoduleService,
    private apiconstantService:ApiconstantService) { }

  ngOnInit(): void {

    this.getUserInfofromWebApi().subscribe(res => {
      this.result = res;
    });

  }
  getUserInfofromWebApi()
  {    
   
     return this.httpHelper.apiGet(this.apiconstantService.endpoint+"get-user",null);
    
  }
  

}
