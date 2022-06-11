import { TestBed } from '@angular/core/testing';

import { ClipyService } from './clipy.service';

describe('ClipyService', () => {
  let service: ClipyService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClipyService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
