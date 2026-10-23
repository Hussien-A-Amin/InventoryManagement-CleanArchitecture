import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientOutlet } from './client-outlet';

describe('ClientOutlet', () => {
  let component: ClientOutlet;
  let fixture: ComponentFixture<ClientOutlet>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ClientOutlet],
    }).compileComponents();

    fixture = TestBed.createComponent(ClientOutlet);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
