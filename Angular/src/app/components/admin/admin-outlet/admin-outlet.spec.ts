import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminOutlet } from './admin-outlet';

describe('AdminOutlet', () => {
  let component: AdminOutlet;
  let fixture: ComponentFixture<AdminOutlet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminOutlet],
    }).compileComponents();

    fixture = TestBed.createComponent(AdminOutlet);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
