import { TestBed } from '@angular/core/testing';

import { ProductsApi } from './products-api';

describe('ProductsApi', () => {
  let service: ProductsApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductsApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
