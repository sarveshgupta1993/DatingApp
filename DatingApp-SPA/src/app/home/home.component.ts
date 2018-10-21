import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  values: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getvalues();
  }

  registertoggle() {
    this.registerMode = true;
  }

  getvalues() {

    this.http.get('http://localhost:53687/api/values').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });

    }
    cancelMode(registerMode) {
      this.registerMode = registerMode;
    }

}
