import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <header class="header">
      <div class="container">
        <div class="header-content">
          <h1 class="logo">
            <a routerLink="/">Plataforma de Cursos</a>
          </h1>
          <nav class="nav">
            <a routerLink="/cursos" routerLinkActive="active">Cursos</a>
            <a routerLink="/estudantes" routerLinkActive="active">Estudantes</a>
            <a routerLink="/matriculas" routerLinkActive="active">Matrículas</a>
            <a routerLink="/pagamentos" routerLinkActive="active">Pagamentos</a>
          </nav>
        </div>
      </div>
    </header>
  `,
  styles: [`
    .header {
      background-color: #007bff;
      color: white;
      padding: 15px 0;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    .header-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
    .logo a {
      color: white;
      text-decoration: none;
      font-size: 24px;
      font-weight: bold;
    }
    .nav {
      display: flex;
      gap: 20px;
    }
    .nav a {
      color: white;
      text-decoration: none;
      padding: 8px 16px;
      border-radius: 4px;
      transition: background-color 0.3s;
    }
    .nav a:hover,
    .nav a.active {
      background-color: rgba(255,255,255,0.2);
    }
  `]
})
export class HeaderComponent { }

