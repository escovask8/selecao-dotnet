import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Curso } from '../models/curso.model';

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  constructor(private api: ApiService) { }

  obterTodos(): Observable<Curso[]> {
    return this.api.get<Curso[]>('/cursos');
  }
}

