import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JourneyComponent } from './journey.component';
import { JourneyRoutingModule } from './journey-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

@NgModule({
  declarations: [
    JourneyComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    JourneyRoutingModule,
    MatAutocompleteModule
  ]
})
export class JourneyModule { }
