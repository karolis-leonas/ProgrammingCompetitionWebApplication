import { Component, OnInit } from '@angular/core';
import { SubmissionModel } from '../../models/submission.model';
import { ProgrammingCompetitionHttpService } from '../../services/programing-competition-http/programing-competition-http.service';
import { ProgrammingTaskModel } from '../../models/programming-task.model';

@Component({
  selector: 'submit',
  templateUrl: './submission-page.component.html',
  styleUrls: ['./submission-page.component.css']
})
export class SubmissionPageComponent implements OnInit {
  public submission: SubmissionModel = null;
  public programmingTasks: ProgrammingTaskModel[] = [];

  constructor(
    private _programmingCompetitionHttpService : ProgrammingCompetitionHttpService
  ) { }
  
  ngOnInit() {
    this.submission = new SubmissionModel();

    this._programmingCompetitionHttpService.getProgrammingTasks().subscribe(
      (programmingTasks: ProgrammingTaskModel[]) => {
        this.programmingTasks = programmingTasks;
      }
    );
  }

  public onSubmissionSubmit(): void {
    this._programmingCompetitionHttpService.saveSubmission(this.submission).subscribe(
      (result) => {
        console.log(this.submission);
        console.log(result);
      },
      (error) => {
        console.error(error);
      }
    )
  }
}
