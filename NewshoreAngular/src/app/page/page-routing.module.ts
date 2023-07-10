import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PageComponent } from './page.component';

const routes: Routes = [
  { path: '', 
    component: PageComponent,
    children: [
      { path: 'flights',
        loadChildren: () => import('../journey/journey-routing.module').then(mod => mod.JourneyRoutingModule)
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
