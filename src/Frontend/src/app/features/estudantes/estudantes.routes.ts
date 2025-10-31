import { Routes } from '@angular/router';
import { EstudantesComponent } from './estudantes.component';
import { CadastrarEstudanteComponent } from './components/cadastrar-estudante/cadastrar-estudante.component';
import { AdicionarCartaoComponent } from './components/adicionar-cartao/adicionar-cartao.component';

export const routes: Routes = [
  {
    path: '',
    component: EstudantesComponent
  },
  {
    path: 'cadastrar',
    component: CadastrarEstudanteComponent
  },
  {
    path: ':id/cartoes/adicionar',
    component: AdicionarCartaoComponent
  }
];

