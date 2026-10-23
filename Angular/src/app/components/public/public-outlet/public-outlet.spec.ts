import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicOutlet } from './public-outlet';

describe('PublicOutlet', () => {
  let component: PublicOutlet;
  let fixture: ComponentFixture<PublicOutlet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PublicOutlet],
    }).compileComponents();

    fixture = TestBed.createComponent(PublicOutlet);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
