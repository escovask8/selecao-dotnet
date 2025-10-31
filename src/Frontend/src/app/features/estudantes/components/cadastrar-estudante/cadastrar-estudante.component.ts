import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EstudanteService } from '../../../../core/services/estudante.service';

@Component({
  selector: 'app-cadastrar-estudante',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="card">
      <h3>Cadastrar Estudante</h3>
      
      <form [formGroup]="form" (ngSubmit)="onSubmit()">
        <div class="form-group">
          <label for="nome">Nome *</label>
          <input 
            id="nome" 
            type="text" 
            formControlName="nome" 
            placeholder="Digite o nome completo">
          <div class="error-message" *ngIf="form.get('nome')?.hasError('required') && form.get('nome')?.touched">
            Nome é obrigatório
          </div>
          <div class="error-message" *ngIf="form.get('nome')?.hasError('minlength') && form.get('nome')?.touched">
            Nome deve ter no mínimo 3 caracteres
          </div>
        </div>

        <div class="form-group">
          <label for="email">Email *</label>
          <input 
            id="email" 
            type="email" 
            formControlName="email" 
            placeholder="Digite o email">
          <div class="error-message" *ngIf="form.get('email')?.hasError('required') && form.get('email')?.touched">
            Email é obrigatório
          </div>
          <div class="error-message" *ngIf="form.get('email')?.hasError('email') && form.get('email')?.touched">
            Email inválido
          </div>
        </div>

        <div class="error-message" *ngIf="errorMessage">
          {{ errorMessage }}
        </div>

        <div class="success-message" *ngIf="successMessage">
          {{ successMessage }}
        </div>

        <div class="form-actions">
          <button type="submit" class="btn btn-primary" [disabled]="form.invalid || loading">
            {{ loading ? 'Cadastrando...' : 'Cadastrar' }}
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
export class CadastrarEstudanteComponent {
  form: FormGroup;
  loading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private fb: FormBuilder,
    private estudanteService: EstudanteService,
    private router: Router
  ) {
    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.form.valid) {
      this.loading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.estudanteService.cadastrar(this.form.value).subscribe({
        next: (estudante) => {
          this.successMessage = `Estudante ${estudante.nome} cadastrado com sucesso!`;
          setTimeout(() => {
            this.router.navigate(['/estudantes']);
          }, 2000);
        },
        error: (error) => {
          this.errorMessage = error.error?.error || 'Erro ao cadastrar estudante';
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

