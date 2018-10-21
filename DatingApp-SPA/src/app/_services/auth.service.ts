import { Injectable } from '@angular/core';
import {HttpClientModule , HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseApi = 'http://localhost:53687/api/auth/';
jwtHelper = new JwtHelperService();
decodetoken: any;
constructor(private http: HttpClient) { }

login(model: any) {

  return this.http.post(this.baseApi + 'login', model).pipe(
    map((response: any) => {
       const user = response;
       if (user) {
       localStorage.setItem('token', user.token);
       this.decodetoken = this.jwtHelper.decodeToken(user.token);
       }

    })
  );

}
loggedin() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

register(model: any) {
 return this.http.post(this.baseApi + 'register' , model);
}


}
