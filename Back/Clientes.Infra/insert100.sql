-- Definir variáveis para contadores e armazenamento de IDs
DECLARE @contador INT = 1;
DECLARE @maxClientes INT = 100;
DECLARE @docId UNIQUEIDENTIFIER;
DECLARE @emailId UNIQUEIDENTIFIER;
DECLARE @enderecoId UNIQUEIDENTIFIER;
DECLARE @telefoneId UNIQUEIDENTIFIER;
DECLARE @clienteId UNIQUEIDENTIFIER;
DECLARE @tipoDoc INT;
DECLARE @numeroDoc NVARCHAR(20);
DECLARE @isento BIT;
DECLARE @inscricaoEstadual NVARCHAR(20);
DECLARE @nome NVARCHAR(200);
DECLARE @dataNascimento DATETIME2;

-- Criar tabelas temporárias para armazenar valores para uso aleatório
CREATE TABLE #NomesPF (
    Id INT IDENTITY(1,1), 
    Nome NVARCHAR(100)
);

CREATE TABLE #NomesPJ (
    Id INT IDENTITY(1,1), 
    Nome NVARCHAR(100)
);

CREATE TABLE #Sobrenomes (
    Id INT IDENTITY(1,1), 
    Sobrenome NVARCHAR(100)
);

CREATE TABLE #Logradouros (
    Id INT IDENTITY(1,1), 
    Tipo NVARCHAR(20),
    Nome NVARCHAR(100)
);

CREATE TABLE #Bairros (
    Id INT IDENTITY(1,1), 
    Nome NVARCHAR(100)
);

CREATE TABLE #Cidades (
    Id INT IDENTITY(1,1), 
    Nome NVARCHAR(100),
    Estado CHAR(2)
);

-- Preencher tabelas com dados para serem usados aleatoriamente
INSERT INTO #NomesPF (Nome) VALUES 
('João'), ('Maria'), ('José'), ('Ana'), ('Carlos'), ('Mariana'), ('Paulo'), ('Fernanda'), ('Pedro'), ('Juliana'),
('Lucas'), ('Amanda'), ('Rodrigo'), ('Patrícia'), ('Marcelo'), ('Camila'), ('Rafael'), ('Aline'), ('Gustavo'), ('Débora');

INSERT INTO #NomesPJ (Nome) VALUES 
('Comércio'), ('Indústria'), ('Distribuidora'), ('Supermercado'), ('Restaurante'), ('Farmácia'), 
('Padaria'), ('Consultoria'), ('Transportes'), ('Serviços');

INSERT INTO #Sobrenomes (Sobrenome) VALUES 
('Silva'), ('Santos'), ('Oliveira'), ('Souza'), ('Pereira'), ('Lima'), ('Costa'), ('Ferreira'), ('Rodrigues'), ('Almeida'),
('Nascimento'), ('Carvalho'), ('Gomes'), ('Martins'), ('Araújo'), ('Ribeiro'), ('Alves'), ('Monteiro'), ('Mendes'), ('Freitas');

INSERT INTO #Logradouros (Tipo, Nome) VALUES 
('Rua', 'das Flores'), ('Avenida', 'Brasil'), ('Rua', 'São Paulo'), ('Avenida', 'Paulista'), 
('Rua', 'Santos Dumont'), ('Avenida', 'Atlântica'), ('Rua', 'Copacabana'), ('Avenida', 'Rio Branco'), 
('Rua', 'Marechal Deodoro'), ('Avenida', 'Getúlio Vargas');

INSERT INTO #Bairros (Nome) VALUES 
('Centro'), ('Jardim América'), ('Vila Nova'), ('Bela Vista'), ('Santa Cecília'),
('Jardim Europa'), ('Ipanema'), ('Botafogo'), ('Boa Viagem'), ('Liberdade');

INSERT INTO #Cidades (Nome, Estado) VALUES 
('São Paulo', 'SP'), ('Rio de Janeiro', 'RJ'), ('Belo Horizonte', 'MG'), ('Salvador', 'BA'), ('Porto Alegre', 'RS'),
('Recife', 'PE'), ('Fortaleza', 'CE'), ('Curitiba', 'PR'), ('Manaus', 'AM'), ('Brasília', 'DF');

-- Iniciar transação
BEGIN TRANSACTION;

-- Loop para inserir 100 clientes com dados relacionados
WHILE @contador <= @maxClientes
BEGIN
    -- Gerar IDs únicos
    SET @docId = NEWID();
    SET @emailId = NEWID();
    SET @enderecoId = NEWID();
    SET @telefoneId = NEWID();
    SET @clienteId = NEWID();
    
    -- Determinar se é pessoa física (1) ou jurídica (2)
    SET @tipoDoc = CASE WHEN @contador % 4 = 0 THEN 2 ELSE 1 END; -- 25% pessoas jurídicas
    
    -- Gerar documento (CPF ou CNPJ)
    IF @tipoDoc = 1
        SET @numeroDoc = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 90000000 + 10000000) + CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 900 + 100);
    ELSE
        SET @numeroDoc = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 90000000 + 10000000) + CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 900000 + 100000);
    
    -- Definir se é isento de inscrição estadual
    SET @isento = CASE 
                    WHEN @tipoDoc = 1 THEN 1 -- Pessoa física geralmente é isento
                    ELSE CASE WHEN ABS(CHECKSUM(NEWID())) % 3 = 0 THEN 1 ELSE 0 END -- 1/3 das empresas são isentas
                  END;
    
    -- Gerar inscrição estadual
    IF @isento = 1
        SET @inscricaoEstadual = '';
    ELSE
        SET @inscricaoEstadual = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 90000000 + 10000000);
    
    -- Gerar nome baseado no tipo de documento
    IF @tipoDoc = 1
    BEGIN
        -- Pessoa física
        DECLARE @nomePF NVARCHAR(50), @sobrenome NVARCHAR(50);
        SELECT @nomePF = Nome FROM #NomesPF WHERE Id = (ABS(CHECKSUM(NEWID())) % 20) + 1;
        SELECT @sobrenome = Sobrenome FROM #Sobrenomes WHERE Id = (ABS(CHECKSUM(NEWID())) % 20) + 1;
        SET @nome = @nomePF + ' ' + @sobrenome;
        
        -- Data de nascimento entre 1950 e 2000
        SET @dataNascimento = DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 18250), '1950-01-01');
    END
    ELSE
    BEGIN
        -- Pessoa jurídica
        DECLARE @nomePJ NVARCHAR(50), @sobrenome1 NVARCHAR(50), @sobrenome2 NVARCHAR(50);
        SELECT @nomePJ = Nome FROM #NomesPJ WHERE Id = (ABS(CHECKSUM(NEWID())) % 10) + 1;
        SELECT @sobrenome1 = Sobrenome FROM #Sobrenomes WHERE Id = (ABS(CHECKSUM(NEWID())) % 20) + 1;
        SELECT @sobrenome2 = Sobrenome FROM #Sobrenomes WHERE Id = (ABS(CHECKSUM(NEWID())) % 20) + 1;
        
        SET @nome = @nomePJ + ' ' + @sobrenome1 + ' e ' + @sobrenome2 + CASE WHEN ABS(CHECKSUM(NEWID())) % 2 = 0 THEN ' Ltda.' ELSE ' S.A.' END;
        
        -- Data de fundação entre 1980 e 2022
        SET @dataNascimento = DATEADD(DAY, (ABS(CHECKSUM(NEWID())) % 15340), '1980-01-01');
    END
    
    -- Inserir Documento
    INSERT INTO [dbo].[Documento] ([Id], [Numero], [Tipo]) 
    VALUES (@docId, @numeroDoc, @tipoDoc);
    
    -- Inserir Email
    DECLARE @emailEndereco NVARCHAR(100);
    IF @tipoDoc = 1
        SET @emailEndereco = LOWER(REPLACE(@nome, ' ', '.')) + '@email.com';
    ELSE
        SET @emailEndereco = 'contato@' + LOWER(REPLACE(REPLACE(REPLACE(REPLACE(@nome, ' ', ''), '.', ''), '&', ''), ',', '')) + '.com.br';
    
    INSERT INTO [dbo].[Email] ([Id], [Endereco])
    VALUES (@emailId, @emailEndereco);
    
    -- Inserir Endereco
    DECLARE @logradouroTipo NVARCHAR(20), @logradouroNome NVARCHAR(100), @bairro NVARCHAR(100);
    DECLARE @cidade NVARCHAR(100), @estado CHAR(2), @numero NVARCHAR(10), @cep NVARCHAR(8);
    
    SELECT @logradouroTipo = Tipo, @logradouroNome = Nome
    FROM #Logradouros
    WHERE Id = (ABS(CHECKSUM(NEWID())) % 10) + 1;
    
    SELECT @bairro = Nome
    FROM #Bairros
    WHERE Id = (ABS(CHECKSUM(NEWID())) % 10) + 1;
    
    SELECT @cidade = Nome, @estado = Estado
    FROM #Cidades
    WHERE Id = (ABS(CHECKSUM(NEWID())) % 10) + 1;
    
    SET @numero = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 2000 + 1);
    SET @cep = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 90000000 + 10000000);
    
    INSERT INTO [dbo].[Endereco] ([Id], [Cep], [Logradouro], [Numero], [Bairro], [Cidade], [Estado])
    VALUES (@enderecoId, @cep, @logradouroTipo + ' ' + @logradouroNome, @numero, @bairro, @cidade, @estado);
    
    -- Inserir Telefone
    DECLARE @telefoneNumero NVARCHAR(20);
    SET @telefoneNumero = CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 90 + 10) + CONVERT(VARCHAR, ABS(CHECKSUM(NEWID())) % 900000000 + 100000000);
    
    INSERT INTO [dbo].[Telefone] ([Id], [Numero])
    VALUES (@telefoneId, @telefoneNumero);
    
    -- Inserir Cliente
    INSERT INTO [dbo].[Clientes] ([Id], [Nome], [DataNascimento], [InscricaoEstadual], [Isento], [Removido], 
                                  [DocumentoId], [EmailId], [EnderecoId], [TelefoneId])
    VALUES (@clienteId, @nome, @dataNascimento, @inscricaoEstadual, @isento, 0, 
            @docId, @emailId, @enderecoId, @telefoneId);
    
    SET @contador = @contador + 1;
END

-- Remover tabelas temporárias
DROP TABLE #NomesPF;
DROP TABLE #NomesPJ;
DROP TABLE #Sobrenomes;
DROP TABLE #Logradouros;
DROP TABLE #Bairros;
DROP TABLE #Cidades;

-- Commit da transação
COMMIT TRANSACTION;

PRINT 'Foram inseridos 100 clientes com sucesso.';