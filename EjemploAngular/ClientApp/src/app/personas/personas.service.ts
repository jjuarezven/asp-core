import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPersona } from '../models/persona';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {
  private apiUrl = this.baseUrl + "api/personas";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getPersonas(): Observable<IPersona[]> {
    return this.http.get<IPersona[]>(this.apiUrl);
  }

  getPersona(personaId: string): Observable<IPersona> {
    const params = new HttpParams().set("incluirDirecciones","true");
    return this.http.get<IPersona>(this.apiUrl + '/' + personaId, {params});
  }

  createPersona(persona: IPersona): Observable<IPersona> {
    return this.http.post<IPersona>(this.apiUrl, persona);
  }

  updatePersona(persona: IPersona): Observable<IPersona> {
    return this.http.put<IPersona>(this.apiUrl + '/' + persona.id.toString(), persona);
  }

  deletePersona(personaId: string): Observable<IPersona> {
    return this.http.delete<IPersona>(this.apiUrl + '/' + personaId);
  }
}
