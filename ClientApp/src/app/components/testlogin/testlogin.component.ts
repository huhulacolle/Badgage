import { StorageService } from './../../services/storage.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-testlogin',
  templateUrl: './testlogin.component.html',
  styleUrls: ['./testlogin.component.css']
})
export class TestloginComponent {

  constructor(
    private storageService: StorageService
  ) { }

  disconnect(): void {
    this.storageService.removeUser();
  }

}
