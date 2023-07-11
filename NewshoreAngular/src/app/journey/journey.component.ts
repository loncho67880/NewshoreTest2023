import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
//import { JournyServiceService } from '../services/journey.service';

@Component({
  selector: 'app-journey',
  templateUrl: './journey.component.html',
  styleUrls: ['./journey.component.scss']
})
export class JourneyComponent {
  myControl = new FormControl('');
  /*
  dateFin: Date | undefined;
  idayvuelta: boolean = false;

  items!: any[];



  Destino: any;

  suggestionsDestino!: any[];

  constructor(private journeyservice: JournyServiceService){
    this.journeyservice.GetCitys().subscribe(cities =>{
      this.items = cities;
    });
  }

  

  searchDestino(event: any): void{
      this.suggestionsDestino = [...Array(10).keys()].map(item => event.query + '-' + item);
  }*/
}
