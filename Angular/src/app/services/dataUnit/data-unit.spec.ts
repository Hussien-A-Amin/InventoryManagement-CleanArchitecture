import { TestBed } from '@angular/core/testing';

import { DataUnit } from './data-unit';

describe('DataUnit', () => {
    let service: DataUnit;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(DataUnit);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
