import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ScoresPageComponent } from './components/scores-page/scores-page.component';
import { SubmissionPageComponent } from './components/submission-page/submission-page.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SubmissionPageComponent,
    ScoresPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'submit', component: SubmissionPageComponent },
      { path: 'scores', component: ScoresPageComponent },
      { path: '',
        redirectTo: `/scores`,
        pathMatch: 'full'
      },
      { path: '**', redirectTo: `/scores` }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
