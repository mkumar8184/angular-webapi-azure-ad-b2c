import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiconstantService { 

  public endpoint: string = 'http://localhost:5199/api/v1/authentication/';
 
 
 
  constructor() { }
}
