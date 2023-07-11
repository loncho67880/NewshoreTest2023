import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageComponent } from './page.component';

const routes: Routes = [
  { path: '', 
    component: PageComponent,
    children: [
      { path: 'flights',
        loadChildren: () => import('../journey/journey.module').then(mod => mod.JourneyModule)
      },
      { path: '',
        redirectTo: 'flights',
        pathMatch: 'full'
      }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }
