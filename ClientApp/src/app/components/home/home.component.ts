import { Component } from '@angular/core';
import { StorageService } from '../../services/storage.service';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(
    private storageService: StorageService,
  ) { }

  

  disconnect(): void {
    this.storageService.removeUser();
  }
}
