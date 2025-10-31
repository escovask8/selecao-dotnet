export interface CartaoCredito {
  id: string;
  estudanteId: string;
  numero: string;
  nomeTitular: string;
  validade: string;
  cvv: string;
  dataCadastro: string;
}

export interface CadastrarCartaoCreditoRequest {
  numero: string;
  nomeTitular: string;
  validade: string;
  cvv: string;
}

