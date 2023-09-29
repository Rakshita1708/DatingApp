import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit 
{
  model: any = {};
  //injecting accountsservice here
  constructor(public accountService: AccountService, public router: Router,private toastr: ToastrService) {}
  ngOnInit(): void
  {

  }
  
  //login method
  login()
  {
    //account service login method
    this.accountService.login(this.model).subscribe(
    {
      next: _ =>
      this.router.navigateByUrl('/members'), //this redirects page after logging in to members page
    })
  }
  logout()
  {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    
  }
}
