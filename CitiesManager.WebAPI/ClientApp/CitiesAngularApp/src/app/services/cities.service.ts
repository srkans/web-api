import { Injectable } from '@angular/core';
import { City } from "../models/city";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

const API_BASE_URL: string = "https://localhost:7209/api/";

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

    return this.httpClient.get<City[]>(`${API_BASE_URL}v1/cities`, { headers: headers });
  }

  public PostCity(city: City): Observable<City> {

    let headers = new HttpHeaders();
    headers = headers.append("Authorization", "Bearer mytoken");

    return this.httpClient.post<City>(`${API_BASE_URL}v1/cities`, city , { headers: headers });
  }
}
