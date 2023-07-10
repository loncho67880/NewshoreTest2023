import { Component } from '@angular/core';
import { JournyServiceService } from '../services/journey.service';

@Component({
  selector: 'app-journey',
  templateUrl: './journey.component.html',
  styleUrls: ['./journey.component.scss']
})
export class JourneyComponent {
  dateIni: Date | undefined;
  dateFin: Date | undefined;
  idayvuelta: boolean = false;

  items!: any[];

  Origen: any;

  suggestionsOrigen!: any[];

  Destino: any;

  suggestionsDestino!: any[];

  constructor(private journeyservice: JournyServiceService){
    this.journeyservice.GetCitys().subscribe(cities =>{
      this.items = cities;
    });
  }

  searchOrigen(event: any): void{
    this.suggestionsDestino = [...Array(10).keys()].map(item => event.query + '-' + item);
  }

  searchDestino(event: any): void{
      this.suggestionsDestino = [...Array(10).keys()].map(item => event.query + '-' + item);
  }
}
