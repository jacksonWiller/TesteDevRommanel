-- Inserir dados na tabela Documento
INSERT INTO "Documento" ("Id", "Numero", "Tipo")
VALUES 
  ('a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890', '12345678900', 1),  -- CPF
  ('b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901', '98765432100', 1),  -- CPF
  ('c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012', '11222333444555', 2),  -- CNPJ
  ('d4e5f6a7-b8c9-0123-d4e5-f6a7b8c90123', '22333444555666', 2),  -- CNPJ
  ('e5f6a7b8-c9d0-1234-e5f6-a7b8c9d01234', '33444555666777', 2);  -- CNPJ
  
  

-- Inserir dados na tabela Email
INSERT INTO "Email" ("Id", "Endereco")
VALUES 
  ('f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12345', 'maria.silva@email.com'),
  ('a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23456', 'joao.santos@email.com'),
  ('b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34567', 'empresa1@empresa.com.br'),
  ('c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45678', 'contato@empresa2.com.br'),
  ('d0e1f2a3-b4c5-6789-d0e1-f2a3b4c56789', 'financeiro@empresa3.com.br');

-- Inserir dados na tabela Endereco
INSERT INTO "Endereco" ("Id", "Cep", "Logradouro", "Numero", "Bairro", "Cidade", "Estado")
VALUES 
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67890', '01001-000', 'Praça da Sé', '123', 'Sé', 'São Paulo', 'SP'),
  ('f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78901', '22021-001', 'Avenida Atlântica', '456', 'Copacabana', 'Rio de Janeiro', 'RJ'),
  ('a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89012', '30130-110', 'Rua dos Carijós', '789', 'Centro', 'Belo Horizonte', 'MG'),
  ('b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90123', '70070-120', 'Esplanada dos Ministérios', '1000', 'Zona Cívico-Administrativa', 'Brasília', 'DF'),
  ('c5d6e7f8-a9b0-1234-c5d6-e7f8a9b01234', '80010-010', 'Rua XV de Novembro', '250', 'Centro', 'Curitiba', 'PR');

-- Inserir dados na tabela Telefone
INSERT INTO "Telefone" ("Id", "Numero")
VALUES 
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12345', '(11) 98765-4321'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23456', '(21) 99876-5432'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34567', '(31) 3333-4444'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45678', '(61) 2222-3333'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a56789', '(41) 4444-5555');

-- Inserir dados na tabela Clientes
INSERT INTO "Clientes" (
  "Id", "Nome", "DocumentoId", "DataNascimento", "TelefoneId", "EmailId", "EnderecoId", 
  "InscricaoEstadual", "Isento", "Removido"
)
VALUES 
  (
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b67890',
    'Maria Silva',
    'a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890',
    '1985-06-15 00:00:00+00',
    'd6e7f8a9-b0c1-2345-d6e7-f8a9b0c12345',
    'f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12345',
    'e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67890',
    '',
    TRUE,
    FALSE
  ),
  (
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c78901',
    'João Santos',
    'b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901',
    '1990-02-28 00:00:00+00',
    'e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23456',
    'a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23456',
    'f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78901',
    '',
    TRUE,
    FALSE
  ),
  (
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d89012',
    'Empresa ABC Ltda',
    'c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012',
    '2010-01-10 00:00:00+00',
    'f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34567',
    'b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34567',
    'a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89012',
    '123456789',
    FALSE,
    FALSE
  ),
  (
    'f4a5b6c7-d8e9-0123-f4a5-b6c7d8e90123',
    'Distribuidora XYZ',
    'd4e5f6a7-b8c9-0123-d4e5-f6a7b8c90123',
    '2005-05-20 00:00:00+00',
    'a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45678',
    'c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45678',
    'b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90123',
    '987654321',
    FALSE,
    FALSE
  ),
  (
    'a5b6c7d8-e9f0-1234-a5b6-c7d8e9f01234',
    'Comércio e Serviços LTDA',
    'e5f6a7b8-c9d0-1234-e5f6-a7b8c9d01234',
    '2015-09-30 00:00:00+00',
    'b0c1d2e3-f4a5-6789-b0c1-d2e3f4a56789',
    'd0e1f2a3-b4c5-6789-d0e1-f2a3b4c56789',
    'c5d6e7f8-a9b0-1234-c5d6-e7f8a9b01234',
    '567891234',
    FALSE,
    TRUE
  );
    