import { Injectable } from '@angular/core';
import {HttpClientModule , HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseApi = 'http://localhost:53687/api/auth/';
constructor(private http: HttpClient) { }

login(model: any) {

  return this.http.post(this.baseApi + 'login', model).pipe(
    map((response: any) => {
       const user = response;
       if (user) {
       localStorage.setItem('token', user.token);
       }

    })
  );

}

register(model: any) {
 return this.http.post(this.baseApi + 'register' , model);
}

}
