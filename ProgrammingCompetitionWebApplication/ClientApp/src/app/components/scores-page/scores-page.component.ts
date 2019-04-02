import { TopSubmitterModel } from './../../models/top-submitter.model';
import { Component, OnInit } from '@angular/core';
import { ProgrammingCompetitionHttpService } from '../../services/programing-competition-http/programing-competition-http.service';

@Component({
  selector: 'scores',
  templateUrl: './scores-page.component.html',
  styleUrls: ['./scores-page.component.css']
})

export class ScoresPageComponent implements OnInit {
  public successfulSubmissions: TopSubmitterModel[];

  constructor(
    private _programmingCompetitionHttpService : ProgrammingCompetitionHttpService
  ) { }

  ngOnInit() {
    this._programmingCompetitionHttpService.getTopSubmitters().subscribe(
      (successfulSubmissions: TopSubmitterModel[]) => {
        this.successfulSubmissions = successfulSubmissions;
      }
    );
  }
}