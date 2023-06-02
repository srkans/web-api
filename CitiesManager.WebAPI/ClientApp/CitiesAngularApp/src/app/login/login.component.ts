import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginUser } from '../models/login-user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm: FormGroup;
  isLoginFormSubmitted: boolean = false;

  constructor(private accountService: AccountService, private router: Router) {
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required]),
      password: new FormControl(null, [Validators.required])
    });
  }
  get login_emailControl(): any {
    return this.loginForm.controls["email"];
  }
  get login_passwordControl(): any {
    return this.loginForm.controls["password"];
  }

  loginSubmitted() {
    this.isLoginFormSubmitted = true;

    this.accountService.postLogin(this.loginForm.value).subscribe({
      next: (response: any) => {
        console.log(response);

        this.isLoginFormSubmitted = false;

        this.accountService.currentUserName = response.email;

        localStorage["token"] = response.token;
        localStorage["refreshToken"] = response.refreshToken;

        this.router.navigate(['/cities']);

        this.loginForm.reset();
      },
      error: (error: any) => {
        console.log(error);
      },
      complete: () => { }
    });
  }
}
