import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Estudante, CadastrarEstudanteRequest } from '../models/estudante.model';
import { CartaoCredito, CadastrarCartaoCreditoRequest } from '../models/cartao-credito.model';

@Injectable({
  providedIn: 'root'
})
export class EstudanteService {
  constructor(private api: ApiService) { }

  cadastrar(request: CadastrarEstudanteRequest): Observable<Estudante> {
    return this.api.post<Estudante>('/estudantes/cadastro', request);
  }

  adicionarCartao(estudanteId: string, request: CadastrarCartaoCreditoRequest): Observable<CartaoCredito> {
    return this.api.post<CartaoCredito>(`/estudantes/${estudanteId}/cartoes`, request);
  }
}

