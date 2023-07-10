import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class JournyServiceService {

    constructor(private http: HttpClient) { }

    GetCitys(): Observable<any> {
        return this.http.get<any>(`${environment.urlservicio}api/Destinos`);
    }
}