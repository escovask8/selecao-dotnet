import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-estudantes',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h2>Estudantes</h2>
        <a routerLink="/estudantes/cadastrar" class="btn btn-primary">Cadastrar Estudante</a>
      </div>
      
      <router-outlet></router-outlet>
    </div>
  `,
  styles: [`
    .page-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 30px;
    }
    .page-header h2 {
      margin: 0;
    }
  `]
})
export class EstudantesComponent { }

