import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalModifyProjectComponent } from './modal-modify-project.component';

describe('ModalModifyProjectComponent', () => {
  let component: ModalModifyProjectComponent;
  let fixture: ComponentFixture<ModalModifyProjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModalModifyProjectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalModifyProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
