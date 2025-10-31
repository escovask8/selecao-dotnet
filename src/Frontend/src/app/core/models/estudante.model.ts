export interface Estudante {
  id: string;
  nome: string;
  email: string;
  dataCadastro: string;
  status: string;
}

export interface CadastrarEstudanteRequest {
  nome: string;
  email: string;
}

