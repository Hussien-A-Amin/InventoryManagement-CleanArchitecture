import { TestBed } from '@angular/core/testing';

import { AlertCenter } from './alert-center';

describe('AlertCenter', () => {
  let service: AlertCenter;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlertCenter);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
