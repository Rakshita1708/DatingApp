import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit 
{
  model: any = {};
  //injecting accountsservice here
  constructor(public accountService: AccountService) {}
  ngOnInit(): void
  {

  }
  
  //login method
  login()
  {
    //account service login method
    this.accountService.login(this.model).subscribe(
      {
      next: response =>
      {
        console.log(response); // what response we get
      },
      error: error => console.log(error) //if there is some problem while logging in this throws error msgx
      
    })
  }
  logout()
  {
    this.accountService.logout();// removing userfrom local storage
    
  }
}
