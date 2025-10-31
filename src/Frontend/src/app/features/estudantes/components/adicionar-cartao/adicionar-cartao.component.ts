import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EstudanteService } from '../../../../core/services/estudante.service';

@Component({
  selector: 'app-adicionar-cartao',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="card">
      <h3>Adicionar Cartão de Crédito</h3>
      
      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="form-group">
          <label for="numero">Número do Cartão *</label>
          <input 
            id="numero" 
            type="text" 
            formControlName="numero" 
            placeholder="1234 5678 9012 3456"
            maxlength="19">
        </div>

        <div class="form-group">
          <label for="nomeTitular">Nome do Titular *</label>
          <input 
            id="nomeTitular" 
            type="text" 
            formControlName="nomeTitular" 
            placeholder="Nome como está no cartão">
        </div>

        <div class="form-group">
          <label for="validade">Validade *</label>
          <input 
            id="validade" 
            type="month" 
            formControlName="validade">
        </div>

        <div class="form-group">
          <label for="cvv">CVV *</label>
          <input 
            id="cvv" 
            type="text" 
            formControlName="cvv" 
            placeholder="123"
            maxlength="4">
        </div>

        <div class="error-message" *ngIf="errorMessage">
          {{ errorMessage }}
        </div>

        <div class="success-message" *ngIf="successMessage">
          {{ successMessage }}
        </div>

        <div class="form-actions">
          <button type="submit" class="btn btn-primary" [disabled]="form.invalid || loading">
            {{ loading ? 'Adicionando...' : 'Adicionar' }}
          </button>
          <button type="button" class="btn btn-secondary" (click)="cancelar()">Cancelar</button>
        </div>
      </form>
    </div>
  `,
  styles: [`
    .form-actions {
      display: flex;
      gap: 10px;
      margin-top: 20px;
    }
  `]
})
export class AdicionarCartaoComponent implements OnInit {
  form: FormGroup;
  estudanteId: string = '';
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private estudanteService: EstudanteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.form = this.fb.group({
      numero: ['', [Validators.required, Validators.pattern(/^\d{13,19}$/)]],
      nomeTitular: ['', [Validators.required, Validators.minLength(3)]],
      validade: ['', [Validators.required]],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3,4}$/)]]
    });
  }

  ngOnInit() {
    this.estudanteId = this.route.snapshot.paramMap.get('id') || '';
  }

  onSubmit() {
    if (this.form.valid) {
      this.loading = true;
      this.errorMessage = '';
      this.successMessage = '';

      const validade = new Date(this.form.value.validade + '-01');
      
      this.estudanteService.adicionarCartao(this.estudanteId, {
        ...this.form.value,
        validade: validade.toISOString()
      }).subscribe({
        next: () => {
          this.successMessage = 'Cartão adicionado com sucesso!';
          setTimeout(() => {
            this.router.navigate(['/estudantes']);
          }, 2000);
        },
        error: (error) => {
          this.errorMessage = error.error?.error || 'Erro ao adicionar cartão';
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        }
      });
    }
  }

  cancelar() {
    this.router.navigate(['/estudantes']);
  }
}

