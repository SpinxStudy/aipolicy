-- Criar um usuário inicial para utilizar na app, se não existir
DO $$
BEGIN
    CREATE ROLE app_user WITH LOGIN PASSWORD 'app#aipolicypwd';
EXCEPTION
    WHEN duplicate_object THEN
        RAISE NOTICE 'Role app_user já existe. Ignorando criação.';
END
$$;

-- Criar o banco de dados se não existir
-- Como o comando CREATE DATABASE não pode ser executado dentro de um bloco transacional,
-- usamos uma consulta que retorna o comando condicionalmente e o \gexec para executá-lo.
SELECT 'CREATE DATABASE pw_tools_db WITH OWNER app_user'
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'pw_tools_db')
\gexec

-- Conecta no banco criado (ou já existente)
\c pw_tools_db

-- Concede privilégios ao usuário (GRANT já ignora se os privilégios já estiverem definidos)
GRANT ALL PRIVILEGES ON DATABASE pw_tools_db TO app_user;

-- Criação da tabela trigger se ela não existir
CREATE TABLE IF NOT EXISTS trigger (
    id SERIAL PRIMARY KEY,
    version INTEGER NOT NULL,
    name VARCHAR(255) NOT NULL,
    active BOOLEAN NOT NULL DEFAULT false,
    attack_valid BOOLEAN NOT NULL DEFAULT false,
    root_conditions VARCHAR(255),
    operations VARCHAR(255)
);

-- Inserção idempotente de dados na tabela trigger
INSERT INTO trigger (id, version, name, active, attack_valid, root_conditions, operations) VALUES
    (1, 1, 'Trigger 1', true, true, 'root_conditions', 'operations'),
    (2, 1, 'Trigger 2', false, true, 'root_conditions', 'operations'),
    (3, 1, 'Trigger 3', true, false, 'root_conditions', 'operations')
ON CONFLICT (id) DO NOTHING;
