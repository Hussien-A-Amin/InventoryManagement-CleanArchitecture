import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortalOutlet } from './portal-outlet';

describe('PortalOutlet', () => {
  let component: PortalOutlet;
  let fixture: ComponentFixture<PortalOutlet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PortalOutlet],
    }).compileComponents();

    fixture = TestBed.createComponent(PortalOutlet);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
