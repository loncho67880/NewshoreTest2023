import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JourneyComponent } from './journey.component';
import { JourneyRoutingModule } from './journey-routing.module';
import { CalendarModule } from 'primeng/calendar';
import { FormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CheckboxModule } from 'primeng/checkbox';
import { InputSwitchModule } from 'primeng/inputswitch';

@NgModule({
  declarations: [
    JourneyComponent
  ],
  imports: [
    CommonModule,
    JourneyRoutingModule,
    CalendarModule,
    FormsModule,
    AutoCompleteModule,
    CheckboxModule,
    InputSwitchModule
  ]
})
export class JourneyModule { }
