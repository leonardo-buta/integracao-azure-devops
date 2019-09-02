import { TestBed } from '@angular/core/testing';

import { WorkItemsService } from './work-items.service';

describe('WorkItemsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: WorkItemsService = TestBed.get(WorkItemsService);
    expect(service).toBeTruthy();
  });
});
