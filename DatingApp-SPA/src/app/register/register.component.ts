import { Component, OnInit, Input, Output , EventEmitter} from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input() valuesfromHome: any;
@Output() cancelledvalue  = new EventEmitter();
  constructor( private authservice: AuthService, private alertifyservice: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(() => {
      this.alertifyservice.success('registration successful');
    },
    error => {
      this.alertifyservice.error(error);
    }
    );

}

cancel() {

  this.cancelledvalue.emit(false);
}

}
