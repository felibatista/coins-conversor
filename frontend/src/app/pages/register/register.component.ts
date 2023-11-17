import { Component } from '@angular/core';
import { RegisterFormComponent } from 'src/app/components/register-form/register-form.component';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [
    RegisterFormComponent
  ],
})
export class RegisterComponent {

}