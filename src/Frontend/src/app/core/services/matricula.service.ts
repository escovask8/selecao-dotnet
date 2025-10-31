import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Matricula, RealizarMatriculaRequest } from '../models/matricula.model';

@Injectable({
  providedIn: 'root'
})
export class MatriculaService {
  constructor(private api: ApiService) { }

  realizarMatricula(request: RealizarMatriculaRequest): Observable<Matricula> {
    return this.api.post<Matricula>('/matriculas', request);
  }
}

