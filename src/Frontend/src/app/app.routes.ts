import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/cursos',
    pathMatch: 'full'
  },
  {
    path: 'estudantes',
    loadChildren: () => import('./features/estudantes/estudantes.routes').then(m => m.routes)
  },
  {
    path: 'cursos',
    loadChildren: () => import('./features/cursos/cursos.routes').then(m => m.routes)
  },
  {
    path: 'matriculas',
    loadChildren: () => import('./features/matriculas/matriculas.routes').then(m => m.routes)
  },
  {
    path: 'pagamentos',
    loadChildren: () => import('./features/pagamentos/pagamentos.routes').then(m => m.routes)
  }
];

