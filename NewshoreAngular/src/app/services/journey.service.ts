import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Journey } from '../models/Journey';

@Injectable({
    providedIn: 'root'
})
export class JournyServiceService {

    constructor(private http: HttpClient) { }

    GetCitys(): Observable<string[]> {
        return this.http.get<string[]>(`${environment.urlservicio}api/Destinos`);
    }

    GetRoutes(origin: string, destination:string): Observable<Journey[]> {
        return this.http.get<Journey[]>(`${environment.urlservicio}api/Route?Origin=${origin}&Destination=${destination}`);
    }
}