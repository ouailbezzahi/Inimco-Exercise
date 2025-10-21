import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPerson, IPersonAnalysis } from '../../../types';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private apiUrl = 'https://localhost:7068/api/person';

  constructor(private http: HttpClient) { }

  analyzePerson(person: IPerson): Observable<IPersonAnalysis> {
    return this.http.post<IPersonAnalysis>(`${this.apiUrl}/analyze`, person);
  }
}
