import { Injectable } from '@angular/core';
import { City } from "../models/city";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  cities: City[] = [];
  constructor(private httpClient:HttpClient) {

  }

  public GetCities(): Observable<City[]> {
    return this.httpClient.get<City[]>("https://localhost:7209/api/v1/cities");
  }
}
