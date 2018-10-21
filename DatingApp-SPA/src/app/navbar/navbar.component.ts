import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { tokenKey } from '@angular/core/src/view';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
model: any = {};
  constructor(public authservice: AuthService , private alertifyservice: AlertifyService) { }

  ngOnInit() {
  }

  login() {
    this.authservice.login(this.model).subscribe(next => {
      this.alertifyservice.success('logged in successful');
    },
    error => {
      this.alertifyservice.error(error);
    }
    );
  }
  loggedin() {
   return this.authservice.loggedin();
  }

  loggedOut() {
       localStorage.removeItem('token');
       this.alertifyservice.message('logged out');
  }



}
