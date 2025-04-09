-- Inserir dados na tabela Documento
INSERT INTO "Documento" ("Id", "Numero", "Tipo")
VALUES 
  -- CPFs (Tipo 1)
  ('a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890', '12345678900', 1),
  ('b2c3d4e5-f6a7-8901-b2c3-d4e5f6a78901', '98765432100', 1),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b67891', '45678912300', 1),
  ('d1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890', '78912345600', 1),
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67891', '32165498700', 1),
  ('f1a2b3c4-d5e6-7890-f1a2-b3c4d5e67890', '65432198700', 1),
  ('a2b3c4d5-e6f7-8901-a2b3-c4d5e6f78901', '15935785200', 1),
  ('b3c4d5e6-f7a8-9012-b3c4-d5e6f7a89012', '95175385200', 1),
  ('c4d5e6f7-a8b9-0123-c4d5-e6f7a8b90123', '75315985200', 1),
  ('d5e6f7a8-b9c0-1234-d5e6-f7a8b9c01234', '35795175300', 1),
  
  -- CNPJs (Tipo 2)
  ('c3d4e5f6-a7b8-9012-c3d4-e5f6a7b89012', '11222333444555', 2),
  ('d4e5f6a7-b8c9-0123-d4e5-f6a7b8c90123', '22333444555666', 2),
  ('e5f6a7b8-c9d0-1234-e5f6-a7b8c9d01234', '33444555666777', 2),
  ('f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12346', '44555666777888', 2),
  ('a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23457', '55666777888999', 2),
  ('b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34568', '66777888999000', 2),
  ('c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45679', '77888999000111', 2),
  ('d0e1f2a3-b4c5-6789-d0e1-f2a3b4c5678a', '88999000111222', 2);
  
-- Inserir dados na tabela Email
INSERT INTO "Email" ("Id", "Endereco")
VALUES 
  ('f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12345', 'maria.silva@email.com'),
  ('a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23456', 'joao.santos@email.com'),
  ('b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34567', 'empresa1@empresa.com.br'),
  ('c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45678', 'contato@empresa2.com.br'),
  ('d0e1f2a3-b4c5-6789-d0e1-f2a3b4c56789', 'financeiro@empresa3.com.br'),
  ('e1f2g3h4-i5j6-7890-e1f2-g3h4i5j67890', 'ana.oliveira@email.com'),
  ('f2g3h4i5-j6k7-8901-f2g3-h4i5j6k78901', 'carlos.souza@email.com'),
  ('g3h4i5j6-k7l8-9012-g3h4-i5j6k7l89012', 'luciana.costa@gmail.com'),
  ('h4i5j6k7-l8m9-0123-h4i5-j6k7l8m90123', 'rafael.lima@hotmail.com'),
  ('i5j6k7l8-m9n0-1234-i5j6-k7l8m9n01234', 'fernanda.alves@outlook.com'),
  ('j6k7l8m9-n0o1-2345-j6k7-l8m9n0o12345', 'bruno.mendes@yahoo.com'),
  ('k7l8m9n0-o1p2-3456-k7l8-m9n0o1p23456', 'juliana.dias@email.com'),
  ('l8m9n0o1-p2q3-4567-l8m9-n0o1p2q34567', 'pedro.martins@gmail.com'),
  ('m9n0o1p2-q3r4-5678-m9n0-o1p2q3r45678', 'vendas@comercio.com.br'),
  ('n0o1p2q3-r4s5-6789-n0o1-p2q3r4s56789', 'atendimento@industria.com.br'),
  ('o1p2q3r4-s5t6-7890-o1p2-q3r4s5t67890', 'contabilidade@servicos.com.br'),
  ('p2q3r4s5-t6u7-8901-p2q3-r4s5t6u78901', 'adm@tecnologia.com.br'),
  ('q3r4s5t6-u7v8-9012-q3r4-s5t6u7v89012', 'rh@consultoria.com.br');

-- Inserir dados na tabela Endereco
INSERT INTO "Endereco" ("Id", "Cep", "Logradouro", "Numero", "Bairro", "Cidade", "Estado")
VALUES 
  ('e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67890', '01001-000', 'Praça da Sé', '123', 'Sé', 'São Paulo', 'SP'),
  ('f2a3b4c5-d6e7-8901-f2a3-b4c5d6e78901', '22021-001', 'Avenida Atlântica', '456', 'Copacabana', 'Rio de Janeiro', 'RJ'),
  ('a3b4c5d6-e7f8-9012-a3b4-c5d6e7f89012', '30130-110', 'Rua dos Carijós', '789', 'Centro', 'Belo Horizonte', 'MG'),
  ('b4c5d6e7-f8a9-0123-b4c5-d6e7f8a90123', '70070-120', 'Esplanada dos Ministérios', '1000', 'Zona Cívico-Administrativa', 'Brasília', 'DF'),
  ('c5d6e7f8-a9b0-1234-c5d6-e7f8a9b01234', '80010-010', 'Rua XV de Novembro', '250', 'Centro', 'Curitiba', 'PR'),
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12346', '40010-000', 'Avenida Sete de Setembro', '555', 'Centro', 'Salvador', 'BA'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23457', '50030-150', 'Avenida Boa Viagem', '1200', 'Boa Viagem', 'Recife', 'PE'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34568', '60170-001', 'Avenida Beira Mar', '300', 'Meireles', 'Fortaleza', 'CE'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45679', '90010-190', 'Rua dos Andradas', '1234', 'Centro Histórico', 'Porto Alegre', 'RS'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a5678a', '04538-132', 'Avenida Brigadeiro Faria Lima', '3477', 'Itaim Bibi', 'São Paulo', 'SP'),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789a', '69050-001', 'Avenida Eduardo Ribeiro', '620', 'Centro', 'Manaus', 'AM'),
  ('d2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890b', '66010-020', 'Avenida Presidente Vargas', '800', 'Campina', 'Belém', 'PA'),
  ('e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901c', '20031-170', 'Avenida Rio Branco', '156', 'Centro', 'Rio de Janeiro', 'RJ'),
  ('f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012d', '88015-600', 'Rua Felipe Schmidt', '315', 'Centro', 'Florianópolis', 'SC'),
  ('a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123e', '13330-000', 'Avenida Brasil', '1500', 'Centro', 'Indaiatuba', 'SP'),
  ('b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f', '59020-100', 'Avenida Prudente de Morais', '744', 'Tirol', 'Natal', 'RN'),
  ('c7d8e9f0-a1b2-3456-c7d8-e9f0a1b2345g', '74015-200', 'Avenida Goiás', '505', 'St. Central', 'Goiânia', 'GO'),
  ('d8e9f0a1-b2c3-4567-d8e9-f0a1b2c3456h', '29050-765', 'Avenida Nossa Senhora da Penha', '2190', 'Santa Luíza', 'Vitória', 'ES');

-- Inserir dados na tabela Telefone
INSERT INTO "Telefone" ("Id", "Numero")
VALUES 
  ('d6e7f8a9-b0c1-2345-d6e7-f8a9b0c12345', '(11) 98765-4321'),
  ('e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23456', '(21) 99876-5432'),
  ('f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34567', '(31) 3333-4444'),
  ('a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45678', '(61) 2222-3333'),
  ('b0c1d2e3-f4a5-6789-b0c1-d2e3f4a56789', '(41) 4444-5555'),
  ('c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789b', '(71) 99765-4321'),
  ('d2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890c', '(81) 98877-6655'),
  ('e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d', '(85) 97766-5544'),
  ('f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012e', '(51) 96655-4433'),
  ('a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123f', '(11) 95544-3322'),
  ('b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234g', '(92) 94433-2211'),
  ('c7d8e9f0-a1b2-3456-c7d8-e9f0a1b2345h', '(91) 93322-1100'),
  ('d8e9f0a1-b2c3-4567-d8e9-f0a1b2c3456i', '(21) 92211-0099'),
  ('e9f0a1b2-c3d4-5678-e9f0-a1b2c3d4567j', '(48) 5555-6666'),
  ('f0a1b2c3-d4e5-6789-f0a1-b2c3d4e5678k', '(19) 3344-5566'),
  ('a1b2c3d4-e5f6-7890-a1b2-c3d4e5f6789l', '(84) 3456-7890'),
  ('b2c3d4e5-f6a7-8901-b2c3-d4e5f6a7890m', '(62) 7890-1234'),
  ('c3d4e5f6-a7b8-9012-c3d4-e5f6a7b8901n', '(27) 0123-4567');

-- Inserir dados na tabela Clientes
INSERT INTO "Clientes" (
  "Id", "Nome", "DocumentoId", "DataNascimento", "TelefoneId", "EmailId", "EnderecoId", 
  "InscricaoEstadual", "Isento", "Removido"
)
VALUES 
  -- Pessoas Físicas
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
    'e3f4g5h6-i7j8-9012-e3f4-g5h6i7j89012',
    'Ana Oliveira',
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b67891',
    '1988-09-22 00:00:00+00',
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789b',
    'e1f2g3h4-i5j6-7890-e1f2-g3h4i5j67890',
    'd6e7f8a9-b0c1-2345-d6e7-f8a9b0c12346',
    '',
    TRUE,
    FALSE
  ),
  (
    'f4g5h6i7-j8k9-0123-f4g5-h6i7j8k90123',
    'Carlos Souza',
    'd1e2f3a4-b5c6-7890-d1e2-f3a4b5c67890',
    '1975-04-10 00:00:00+00',
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890c',
    'f2g3h4i5-j6k7-8901-f2g3-h4i5j6k78901',
    'e7f8a9b0-c1d2-3456-e7f8-a9b0c1d23457',
    '',
    TRUE,
    FALSE
  ),
  (
    'g5h6i7j8-k9l0-1234-g5h6-i7j8k9l01234',
    'Luciana Costa',
    'e1f2a3b4-c5d6-7890-e1f2-a3b4c5d67891',
    '1992-11-05 00:00:00+00',
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901d',
    'g3h4i5j6-k7l8-9012-g3h4-i5j6k7l89012',
    'f8a9b0c1-d2e3-4567-f8a9-b0c1d2e34568',
    '',
    TRUE,
    FALSE
  ),
  (
    'h6i7j8k9-l0m1-2345-h6i7-j8k9l0m12345',
    'Rafael Lima',
    'f1a2b3c4-d5e6-7890-f1a2-b3c4d5e67890',
    '1982-07-18 00:00:00+00',
    'f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012e',
    'h4i5j6k7-l8m9-0123-h4i5-j6k7l8m90123',
    'a9b0c1d2-e3f4-5678-a9b0-c1d2e3f45679',
    '',
    TRUE,
    FALSE
  ),
  (
    'i7j8k9l0-m1n2-3456-i7j8-k9l0m1n23456',
    'Fernanda Alves',
    'a2b3c4d5-e6f7-8901-a2b3-c4d5e6f78901',
    '1995-03-25 00:00:00+00',
    'a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123f',
    'i5j6k7l8-m9n0-1234-i5j6-k7l8m9n01234',
    'b0c1d2e3-f4a5-6789-b0c1-d2e3f4a5678a',
    '',
    TRUE,
    FALSE
  ),
  (
    'j8k9l0m1-n2o3-4567-j8k9-l0m1n2o34567',
    'Bruno Mendes',
    'b3c4d5e6-f7a8-9012-b3c4-d5e6f7a89012',
    '1980-12-03 00:00:00+00',
    'b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234g',
    'j6k7l8m9-n0o1-2345-j6k7-l8m9n0o12345',
    'c1d2e3f4-a5b6-7890-c1d2-e3f4a5b6789a',
    '',
    TRUE,
    FALSE
  ),
  (
    'k9l0m1n2-o3p4-5678-k9l0-m1n2o3p45678',
    'Juliana Dias',
    'c4d5e6f7-a8b9-0123-c4d5-e6f7a8b90123',
    '1987-05-30 00:00:00+00',
    'c7d8e9f0-a1b2-3456-c7d8-e9f0a1b2345h',
    'k7l8m9n0-o1p2-3456-k7l8-m9n0o1p23456',
    'd2e3f4a5-b6c7-8901-d2e3-f4a5b6c7890b',
    '',
    TRUE,
    FALSE
  ),
  (
    'l0m1n2o3-p4q5-6789-l0m1-n2o3p4q56789',
    'Pedro Martins',
    'd5e6f7a8-b9c0-1234-d5e6-f7a8b9c01234',
    '1978-08-20 00:00:00+00',
    'd8e9f0a1-b2c3-4567-d8e9-f0a1b2c3456i',
    'l8m9n0o1-p2q3-4567-l8m9-n0o1p2q34567',
    'e3f4a5b6-c7d8-9012-e3f4-a5b6c7d8901c',
    '',
    TRUE,
    FALSE
  ),
  
  -- Pessoas Jurídicas
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
  ),
  (
    'm1n2o3p4-q5r6-7890-m1n2-o3p4q5r67890',
    'Indústria Brasileira S.A.',
    'f6a7b8c9-d0e1-2345-f6a7-b8c9d0e12346',
    '2000-11-15 00:00:00+00',
    'e9f0a1b2-c3d4-5678-e9f0-a1b2c3d4567j',
    'm9n0o1p2-q3r4-5678-m9n0-o1p2q3r45678',
    'f4a5b6c7-d8e9-0123-f4a5-b6c7d8e9012d',
    '123789456',
    FALSE,
    FALSE
  ),
  (
    'n2o3p4q5-r6s7-8901-n2o3-p4q5r6s78901',
    'Tech Solutions ME',
    'a7b8c9d0-e1f2-3456-a7b8-c9d0e1f23457',
    '2017-03-05 00:00:00+00',
    'f0a1b2c3-d4e5-6789-f0a1-b2c3d4e5678k',
    'n0o1p2q3-r4s5-6789-n0o1-p2q3r4s56789',
    'a5b6c7d8-e9f0-1234-a5b6-c7d8e9f0123e',
    '456123789',
    FALSE,
    FALSE
  ),
  (
    'o3p4q5r6-s7t8-9012-o3p4-q5r6s7t89012',
    'Consultoria Empresarial Ltda',
    'b8c9d0e1-f2a3-4567-b8c9-d0e1f2a34568',
    '2012-08-20 00:00:00+00',
    'a1b2c3d4-e5f6-7890-a1b2-c3d4e5f6789l',
    'o1p2q3r4-s5t6-7890-o1p2-q3r4s5t67890',
    'b6c7d8e9-f0a1-2345-b6c7-d8e9f0a1234f',
    '789456123',
    FALSE,
    FALSE
  ),
  (
    'p4q5r6s7-t8u9-0123-p4q5-r6s7t8u90123',
    'Agropecuária Rural Ltda',
    'c9d0e1f2-a3b4-5678-c9d0-e1f2a3b45679',
    '2008-04-10 00:00:00+00',
    'b2c3d4e5-f6a7-8901-b2c3-d4e5f6a7890m',
    'p2q3r4s5-t6u7-8901-p2q3-r4s5t6u78901',
    'c7d8e9f0-a1b2-3456-c7d8-e9f0a1b2345g',
    '321654987',
    FALSE,
    FALSE
  ),
  (
    'q5r6s7t8-u9v0-1234-q5r6-s7t8u9v01234',
    'Transportes Expressos S.A.',
    'd0e1f2a3-b4c5-6789-d0e1-f2a3b4c5678a',
    '2003-12-25 00:00:00+00',
    'c3d4e5f6-a7b8-9012-c3d4-e5f6a7b8901n',
    'q3r4s5t6-u7v8-9012-q3r4-s5t6u7v89012',
    'd8e9f0a1-b2c3-4567-d8e9-f0a1b2c3456h',
    '654987321',
    FALSE,
    FALSE
  );