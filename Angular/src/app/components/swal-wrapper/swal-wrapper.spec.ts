import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwalWrapper } from './swal-wrapper';

describe('SwalWrapper', () => {
  let component: SwalWrapper;
  let fixture: ComponentFixture<SwalWrapper>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SwalWrapper],
    }).compileComponents();

    fixture = TestBed.createComponent(SwalWrapper);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
