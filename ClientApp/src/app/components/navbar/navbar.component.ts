import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private router: Router, private storageService: StorageService) { }

  goToHome(): void {
    this.router.navigateByUrl("/home");
  }

  goToTickets(): void {
    this.router.navigateByUrl("/tickets");
  }

  goToProjets(): void {
    this.router.navigateByUrl("/projets");
  }

  disconnect(): void {
    this.storageService.removeUser();
  }
}
