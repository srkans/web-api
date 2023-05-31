import { Injectable } from '@angular/core';
import { City } from "../models/city";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CitiesService {

  cities: City[] = [];
  constructor(private httpClient:HttpClient) {

  }

  public GetCities(): Observable<City[]> {

    let headers = new HttpHeaders();
    headers = headers.append("Authorization", "Bearer mytoken");

    return this.httpClient.get<City[]>("https://localhost:7209/api/v1/cities", {headers:headers});
  }
}
