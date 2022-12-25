import { User } from './../../client/badgageClient';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/client/badgageClient';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { bounceInLeftOnEnterAnimation, bounceOutLeftOnLeaveAnimation, bounceInRightOnEnterAnimation, bounceOutRightOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  animations: [
    bounceInLeftOnEnterAnimation({anchor: 'enterL', delay : 100,animateChildren: 'together'}),
    bounceOutLeftOnLeaveAnimation({anchor: 'leaveL', animateChildren:'together'}),
    bounceInRightOnEnterAnimation({anchor: 'enterR', animateChildren: 'before'}),
  ]
})
export class RegisterComponent {

  constructor(
    private authService: AuthService,
    private storageService: StorageService,
    private router: Router
  ) 
  email!: string;
  mdp!: string;
  nom!: string;
  prenom!: string;
  naissance!: string;
  loginPage: true;
  register(): void {
    const user = new User;
    user.adresseMail = this.email;
    user.dateNaiss = new Date(this.naissance);
    user.mdp = this.mdp;
    user.nom = this.nom;
    user.prenom = this.prenom;

    this.authService.register(user)
    .then(
      () => {
        this.login();
      }
    )
    .catch(
      err => {
        console.error(err);
      }
    )
  }

  toLogin(): void {
    this.loginPage = true;
  }

  toRegister(): void {
    this.loginPage = false;
  }

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
