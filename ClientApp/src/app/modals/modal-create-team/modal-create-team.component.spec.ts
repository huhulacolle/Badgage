import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalCreateTeamComponent } from './modal-create-team.component';

describe('ModalCreateTeamComponent', () => {
  let component: ModalCreateTeamComponent;
  let fixture: ComponentFixture<ModalCreateTeamComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalCreateTeamComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalCreateTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
