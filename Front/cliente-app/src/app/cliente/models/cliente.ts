export enum TipoDocumento {
  CPF = 0,
  CNPJ = 1
}

export interface Cliente {
  id: string;
  nome: string;
  documento: string;
  tipoDocumento: TipoDocumento;
  dataNascimento: string;
  telefone: string;
  email: string;
  cep: string;
  logradouro: string;
  numero: string;
  bairro: string;
  cidade: string;
  estado: string;
  inscricaoEstadual: string;
  isento: boolean;
  removido?: boolean;
}