export interface Pagamento {
  id: string;
  estudanteId: string;
  cartaoCreditoId?: string;
  valor: number;
  dataPagamento: string;
  status: string;
  descricao: string;
}

