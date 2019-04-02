/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ProgrammingCompetitionHttpService } from './programming-competition-http.service';

describe('Service: ProgrammingCompetition', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProgrammingCompetitionHttpService]
    });
  });

  it('should ...', inject([ProgrammingCompetitionHttpService], (service: ProgrammingCompetitionHttpService) => {
    expect(service).toBeTruthy();
  }));
});
