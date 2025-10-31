import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-matriculas',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container">
      <div class="page-header">
        <h2>Matrículas</h2>
      </div>
      
      <div class="card">
        <p>Funcionalidade de listagem de matrículas em desenvolvimento.</p>
        <p>As matrículas podem ser realizadas na página de cursos.</p>
      </div>
    </div>
  `,
  styles: [`
    .page-header {
      margin-bottom: 30px;
    }
  `]
})
export class MatriculasComponent implements OnInit {
  ngOnInit() {
    // Implementar listagem de matrículas
  }
}

