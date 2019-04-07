import { TopSubmitterModel } from './../../models/top-submitter.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, } from 'rxjs';
import { map } from 'rxjs/operators';
import { SubmissionModel } from '../../models/submission.model';
import { ProgrammingTaskModel } from '../../models/programming-task.model';
import { SubmissionResultModel } from '../../models/submission-result.model';

@Injectable({
  providedIn: 'root'
})
export class ProgrammingCompetitionHttpService {
  controllerUrl: string;

  constructor(private _httpClient: HttpClient, @Inject('BASE_URL') _baseUrl: string) {
    this.controllerUrl = `${_baseUrl}api/Submissions`;
   }

  public getTopSubmitters(): Observable<TopSubmitterModel[]> {
    return this._httpClient.get(`${this.controllerUrl}/TopSubmitters`).pipe(
      map((topSubmitterItems: any) => {
        const mappedTopSubmitters: TopSubmitterModel[] = [];

        topSubmitterItems.forEach(topSubmitterItem => {
          const mappedTopSubmitterItem: TopSubmitterModel = {
            nickname: topSubmitterItem.nickname,
            successfulSubmissions: topSubmitterItem.successfulSubmissions,
            solvedTasks: topSubmitterItem.solvedTasks
          };

          mappedTopSubmitters.push(mappedTopSubmitterItem);
        });

        return mappedTopSubmitters;
      })
    )
  };

  public getProgrammingTasks(): Observable<ProgrammingTaskModel[]> {
    return this._httpClient.get(`${this.controllerUrl}/ProgrammingTasks`).pipe(
      map((programmingTasks: any) => {
        const mappedprogrammingTasks: ProgrammingTaskModel[] = [];

        programmingTasks.forEach(programmingTask => {
          const mappedTopSubmitterItem: ProgrammingTaskModel = {
            taskId: programmingTask.taskId,
            taskName: programmingTask.taskName
          };
          
          mappedprogrammingTasks.push(mappedTopSubmitterItem);
        });

        return mappedprogrammingTasks;
      })
    )
  };

  public saveSubmission(baseSubmissionDto: SubmissionModel): Observable<SubmissionResultModel> {
    return this._httpClient.post(`${this.controllerUrl}/ExecuteSubmission`, baseSubmissionDto).pipe(
      map((submissionResult: any) => {
        const mappedSubmissionResult: SubmissionResultModel = {
          response: submissionResult.response,
          isSubmissionAccepted: submissionResult.isSubmissionAccepted,
          isCorrectResult: submissionResult.isCorrectResult
        };

        return mappedSubmissionResult;
      })
    );
  }
}
