import { StorageService } from './../../services/storage.service';
import { UserLogin } from './../../client/badgageClient';
import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
  ) { }


  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.router.navigateByUrl("/test");
    }
  }

  email!: string;
  mdp!: string;

  login(): void {
    const user = new UserLogin;
    user.adresseMail = this.email;
    user.mdp = this.mdp;
    this.authService.login(user)
    .then(
      data => {
        this.storageService.saveUser(data);
        this.router.navigateByUrl("/test");
      }
    )
    .catch(
      err => {
        console.error(err);
      }
    )
  }

}
