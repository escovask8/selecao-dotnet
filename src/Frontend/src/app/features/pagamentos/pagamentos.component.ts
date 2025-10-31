import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PagamentoService } from '../../core/services/pagamento.service';
import { Pagamento } from '../../core/models/pagamento.model';

@Component({
  selector: 'app-pagamentos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h2>Histórico de Pagamentos</h2>
      </div>

      <div class="card">
        <div class="form-group">
          <label for="estudanteId">ID do Estudante</label>
          <input 
            id="estudanteId" 
            type="text" 
            [(ngModel)]="estudanteId" 
            placeholder="Digite o ID do estudante">
          <button class="btn btn-primary" (click)="carregarPagamentos()" style="margin-top: 10px;">
            Buscar Pagamentos
          </button>
        </div>
      </div>

      <div *ngIf="loading" class="loading">
        Carregando pagamentos...
      </div>

      <div *ngIf="errorMessage" class="error-message card">
        {{ errorMessage }}
      </div>

      <div *ngIf="pagamentos.length > 0" class="pagamentos-list">
        <div class="card" *ngFor="let pagamento of pagamentos">
          <div class="pagamento-header">
            <h4>Pagamento #{{ pagamento.id.substring(0, 8) }}</h4>
            <span class="status" [class.aprovado]="pagamento.status === 'Aprovado'">
              {{ pagamento.status }}
            </span>
          </div>
          <p><strong>Valor:</strong> R$ {{ pagamento.valor.toFixed(2) }}</p>
          <p><strong>Data:</strong> {{ pagamento.dataPagamento | date:'dd/MM/yyyy HH:mm' }}</p>
          <p><strong>Descrição:</strong> {{ pagamento.descricao }}</p>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .pagamentos-list {
      display: flex;
      flex-direction: column;
      gap: 15px;
    }
    .pagamento-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 10px;
    }
    .status {
      padding: 5px 10px;
      border-radius: 4px;
      font-size: 12px;
      font-weight: bold;
      background-color: #ffc107;
      color: #333;
    }
    .status.aprovado {
      background-color: #28a745;
      color: white;
    }
    .loading {
      text-align: center;
      padding: 40px;
    }
    .page-header {
      margin-bottom: 30px;
    }
  `]
})
export class PagamentosComponent implements OnInit {
  pagamentos: Pagamento[] = [];
  estudanteId = '';
  loading = false;
  errorMessage = '';

  constructor(private pagamentoService: PagamentoService) { }

  ngOnInit() {
    // Implementar lógica inicial
  }

  carregarPagamentos() {
    if (!this.estudanteId) {
      this.errorMessage = 'Por favor, informe o ID do estudante';
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.pagamentos = [];

    this.pagamentoService.obterPorEstudante(this.estudanteId).subscribe({
      next: (pagamentos) => {
        this.pagamentos = pagamentos;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Erro ao carregar pagamentos';
        this.loading = false;
      }
    });
  }
}

