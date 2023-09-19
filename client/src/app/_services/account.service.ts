import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$= this.currentUserSource.asObservable();
  /*Http Client Performs HTTP requests. This service is available as an injectable class,  with methods to perform HTTP requests. Each request method has multiple signatures, 
  and the return type varies based on the signature that is called (mainly the values of observe and responseType).*/
  constructor(private http: HttpClient) { }
    
  login(model: any)
  {
    //nav component services are injectable
    //http client post method
    //Observable of the response, with the response body as an object parsed from JSON.
    //console.log(this.baseUrl,model);
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe
    (map((response:User)=> 
      {
        const user = response;
        if(user)
        {
          //storing the user dto in localstorage browser
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      } )
    )
  }


  register(model: any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe
    (map(user => 
      {
        if (user){
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      }))
      
  }
  setCurrentUser(user: User){
    this.currentUserSource.next(user);
  }
  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
  
}

