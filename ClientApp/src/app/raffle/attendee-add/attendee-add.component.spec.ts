import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendeeAddComponent } from './attendee-add.component';

describe('AttendeeAddComponent', () => {
  let component: AttendeeAddComponent;
  let fixture: ComponentFixture<AttendeeAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AttendeeAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendeeAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
