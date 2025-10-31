import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { Pagamento } from '../models/pagamento.model';

@Injectable({
  providedIn: 'root'
})
export class PagamentoService {
  constructor(private api: ApiService) { }

  obterPorEstudante(estudanteId: string): Observable<Pagamento[]> {
    return this.api.get<Pagamento[]>(`/pagamentos/estudante/${estudanteId}`);
  }
}

