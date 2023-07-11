import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { JournyServiceService } from '../services/journey.service';
import { Observable, map, startWith } from 'rxjs';
import { Journey } from '../models/Journey';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-journey',
  templateUrl: './journey.component.html',
  styleUrls: ['./journey.component.scss']
})
export class JourneyComponent implements OnInit {
  OrigenControl = new FormControl<string>('');
  optionsOrigen: string[] = [];
  filteredOrigen: Observable<string[]> | undefined;

  DestinoControl = new FormControl<string>('');
  optionsDestino: string[] = [];
  filteredDestino: Observable<string[]> | undefined;

  idayvuelta: boolean = false;
  ctrlidayvuelta = new FormControl(true);

  journies: Journey[] = [];
  origen: string = "";
  destino: string = "";

  currencies: string[] = ["USD", "Pesos"];
  currency: string = "USD";
  currencyControl = new FormControl<string>('');

  constructor(private journeyservice: JournyServiceService,
              private _snackBar: MatSnackBar){
    this.journeyservice.GetCitys().subscribe(cities =>{
      this.optionsOrigen = cities;
      this.optionsDestino = cities;
    });

    this.ctrlidayvuelta.valueChanges.subscribe(x=>{
      this.idayvuelta = Boolean(x);
    });


    this.OrigenControl.valueChanges.subscribe(x=>{
      this.origen = String(x);
    });

    this.DestinoControl.valueChanges.subscribe(x=>{
      this.destino = String(x);
    });

    this.currencyControl.setValue("USD");
    this.ctrlidayvuelta.setValue(false);
  }

  ngOnInit() {
    this.filteredOrigen = this.OrigenControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value;
        return name ? this._filterOrigen(name as string) : this.optionsOrigen.slice();
      }),
    );

    this.filteredDestino = this.DestinoControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const name = typeof value === 'string' ? value : value;
        return name ? this._filterDestino(name as string) : this.optionsDestino.slice();
      }),
    );
  }

  displayFnOrigen(name: string): string {
    return name && name ? name : '';
  }

  private _filterOrigen(name: string): string[] {
    const filterValue = name.toLowerCase();

    return this.optionsOrigen.filter(option => option.toLowerCase().includes(filterValue));
  }

  displayFnDestino(name: string): string {
    return name && name ? name : '';
  }

  private _filterDestino(name: string): string[] {
    const filterValue = name.toLowerCase();

    return this.optionsDestino.filter(option => option.toLowerCase().includes(filterValue));
  }

  buscarVuelo(){
    if(this.origen.length == 0 || this.destino.length == 0){
      this.openSnackBar("Selecciona origen y destino", "Error")
      return;
    }

    if(this.origen == this.destino){
      this.openSnackBar("No puedes ingresar el mismo lugar en origen y destino", "Error")
      return;
    }

    if(this.origen.length > 3 || this.destino.length > 3){
      this.openSnackBar("No puedes ingresar mas de 3 caracteres", "Error")
      return;
    }

    this.journeyservice.GetRoutes(this.origen, this.destino).subscribe(data=>{
      this.journies = data;
      this.journies.forEach(route=>{
        route.srcimage = `assets/images/${route.destination}.jpg`;
      });
      this.changeCurrency(this.currency);
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action);
  }

  changeCurrency(cur:string){
    this.currency = cur;
    this.buscarVuelo();
    if(this.currency != "USD"){
      this.journies.forEach(route=>{
        route.price = route.price * 4000;
        route.flights.forEach(flight=>{
          flight.price = flight.price * 4000;
        });
      });
    }
  }
}