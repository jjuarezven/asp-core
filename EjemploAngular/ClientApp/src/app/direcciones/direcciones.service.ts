import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DireccionesService {

  private apiUrl = this.baseUrl + "api/direcciones";

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  deleteDirecciones(ids: number[]): Observable<void> {
    return this.http.post<void>(this.apiUrl + "/delete/list", ids);
  }

}
