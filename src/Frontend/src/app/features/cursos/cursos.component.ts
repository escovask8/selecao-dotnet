import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CursoService } from '../../core/services/curso.service';
import { Curso } from '../../core/models/curso.model';
import { MatriculaService } from '../../core/services/matricula.service';
import { EstudanteService } from '../../core/services/estudante.service';
import { Estudante } from '../../core/models/estudante.model';

@Component({
  selector: 'app-cursos',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h2>Cursos Disponíveis</h2>
      </div>

      <div *ngIf="loading" class="loading">
        Carregando cursos...
      </div>

      <div *ngIf="errorMessage" class="error-message card">
        {{ errorMessage }}
      </div>

      <div class="cursos-grid" *ngIf="!loading && !errorMessage">
        <div class="curso-card card" *ngFor="let curso of cursos">
          <h3>{{ curso.titulo }}</h3>
          <p class="professor">Professor: {{ curso.professor }}</p>
          <p class="descricao">{{ curso.descricao }}</p>
          <div class="curso-info">
            <span class="duracao">Duração: {{ curso.duracao }}h</span>
            <span class="preco">R$ {{ curso.preco.toFixed(2) }}</span>
          </div>
          <button 
            class="btn btn-primary" 
            (click)="matricular(curso)"
            [disabled]="!estudanteSelecionado || matriculando">
            {{ matriculando ? 'Matriculando...' : 'Matricular-se' }}
          </button>
        </div>
      </div>

      <div class="card" *ngIf="!estudanteSelecionado">
        <p>Para realizar matrícula, você precisa primeiro cadastrar um estudante.</p>
        <a routerLink="/estudantes/cadastrar" class="btn btn-primary">Cadastrar Estudante</a>
      </div>
    </div>
  `,
  styles: [`
    .loading {
      text-align: center;
      padding: 40px;
    }
    .cursos-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
      gap: 20px;
    }
    .curso-card {
      display: flex;
      flex-direction: column;
    }
    .curso-card h3 {
      margin-bottom: 10px;
      color: #007bff;
    }
    .professor {
      color: #666;
      font-size: 14px;
      margin-bottom: 10px;
    }
    .descricao {
      flex: 1;
      margin-bottom: 15px;
      color: #555;
    }
    .curso-info {
      display: flex;
      justify-content: space-between;
      margin-bottom: 15px;
      padding-top: 15px;
      border-top: 1px solid #eee;
    }
    .preco {
      font-weight: bold;
      font-size: 18px;
      color: #28a745;
    }
    .duracao {
      color: #666;
    }
    .page-header {
      margin-bottom: 30px;
    }
    .page-header h2 {
      margin: 0;
    }
  `]
})
export class CursosComponent implements OnInit {
  cursos: Curso[] = [];
  loading = true;
  errorMessage = '';
  matriculando = false;
  estudanteSelecionado: Estudante | null = null;

  constructor(
    private cursoService: CursoService,
    private matriculaService: MatriculaService,
    private estudanteService: EstudanteService
  ) { }

  ngOnInit() {
    this.carregarCursos();
    // TODO: Implementar seleção de estudante (pode ser via localStorage ou seleção)
  }

  carregarCursos() {
    this.loading = true;
    this.cursoService.obterTodos().subscribe({
      next: (cursos) => {
        this.cursos = cursos;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Erro ao carregar cursos';
        this.loading = false;
      }
    });
  }

  matricular(curso: Curso) {
    if (!this.estudanteSelecionado) {
      alert('Por favor, cadastre um estudante primeiro');
      return;
    }

    this.matriculando = true;
    this.matriculaService.realizarMatricula({
      estudanteId: this.estudanteSelecionado.id,
      cursoId: curso.id
    }).subscribe({
      next: (matricula) => {
        alert(`Matrícula realizada com sucesso! Curso: ${curso.titulo}`);
        this.matriculando = false;
      },
      error: (error) => {
        alert(error.error?.error || 'Erro ao realizar matrícula');
        this.matriculando = false;
      }
    });
  }
}

