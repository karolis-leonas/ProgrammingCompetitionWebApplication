import { Component, OnInit } from '@angular/core';
import { SubmissionModel } from '../../models/submission.model';
import { ProgrammingCompetitionHttpService } from '../../services/programing-competition-http/programing-competition-http.service';
import { ProgrammingTaskModel } from '../../models/programming-task.model';
import { SubmissionResultModel } from '../../models/submission-result.model';

@Component({
  selector: 'submit',
  templateUrl: './submission-page.component.html',
  styleUrls: ['./submission-page.component.css']
})
export class SubmissionPageComponent implements OnInit {
  public submission: SubmissionModel = null;
  public submissionResult: SubmissionResultModel = null;
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
      (submissionResult: SubmissionResultModel) => {
        this.submissionResult = submissionResult;
      },
      (error) => {
        console.error(error);
      }
    )
  }
}
