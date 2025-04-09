CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE TABLE "Documento" (
        "Id" uuid NOT NULL,
        "Numero" text NOT NULL,
        "Tipo" integer NOT NULL,
        CONSTRAINT "PK_Documento" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE TABLE "Email" (
        "Id" uuid NOT NULL,
        "Endereco" text NOT NULL,
        CONSTRAINT "PK_Email" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE TABLE "Endereco" (
        "Id" uuid NOT NULL,
        "Cep" text NOT NULL,
        "Logradouro" text NOT NULL,
        "Numero" text NOT NULL,
        "Bairro" text NOT NULL,
        "Cidade" text NOT NULL,
        "Estado" text NOT NULL,
        CONSTRAINT "PK_Endereco" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE TABLE "Telefone" (
        "Id" uuid NOT NULL,
        "Numero" text NOT NULL,
        CONSTRAINT "PK_Telefone" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE TABLE "Clientes" (
        "Id" uuid NOT NULL,
        "Nome" text NOT NULL,
        "DocumentoId" uuid NOT NULL,
        "DataNascimento" timestamp with time zone NOT NULL,
        "TelefoneId" uuid NOT NULL,
        "EmailId" uuid NOT NULL,
        "EnderecoId" uuid NOT NULL,
        "InscricaoEstadual" text NOT NULL,
        "Isento" boolean NOT NULL,
        "Removido" boolean NOT NULL,
        CONSTRAINT "PK_Clientes" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Clientes_Documento_DocumentoId" FOREIGN KEY ("DocumentoId") REFERENCES "Documento" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Clientes_Email_EmailId" FOREIGN KEY ("EmailId") REFERENCES "Email" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Clientes_Endereco_EnderecoId" FOREIGN KEY ("EnderecoId") REFERENCES "Endereco" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_Clientes_Telefone_TelefoneId" FOREIGN KEY ("TelefoneId") REFERENCES "Telefone" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE INDEX "IX_Clientes_DocumentoId" ON "Clientes" ("DocumentoId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE INDEX "IX_Clientes_EmailId" ON "Clientes" ("EmailId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE INDEX "IX_Clientes_EnderecoId" ON "Clientes" ("EnderecoId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    CREATE INDEX "IX_Clientes_TelefoneId" ON "Clientes" ("TelefoneId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20250409171721_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20250409171721_Initial', '9.0.3');
    END IF;
END $EF$;
COMMIT;

