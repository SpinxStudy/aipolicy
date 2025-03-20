-- Criando usuario para aplicacao 
DO $$
BEGIN
    CREATE ROLE app_user WITH LOGIN PASSWORD 'app#aipolicypwd';
EXCEPTION
    WHEN duplicate_object THEN
        RAISE NOTICE 'Role app_user já existe.';
END
$$;

-- Criando banco de dados
SELECT 'CREATE DATABASE pw_tools_db WITH OWNER app_user'
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'pw_tools_db')
\gexec

\c pw_tools_db

-- Concede privilégios no banco para o app_user
GRANT ALL PRIVILEGES ON DATABASE pw_tools_db TO app_user;

-- Criando tabela Trigger no schema public
CREATE TABLE IF NOT EXISTS public."trigger" (
    id SERIAL PRIMARY KEY,
    version INTEGER NOT NULL,
    name VARCHAR(255) NOT NULL,
    active BOOLEAN NOT NULL DEFAULT false,
    attack_valid BOOLEAN NOT NULL DEFAULT false,
    root_conditions VARCHAR(255),
    operations VARCHAR(255)
);

-- Concede privilégios para ao app_user
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO app_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON TABLES TO app_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON SEQUENCES TO app_user;

-- Mocking de dados
INSERT INTO public."trigger" (id, version, name, active, attack_valid, root_conditions, operations) VALUES
    (1, 1, 'Trigger 1', true, true, 'root_conditions', 'operations'),
    (2, 1, 'Trigger 2', false, true, 'root_conditions', 'operations'),
    (3, 1, 'Trigger 3', true, false, 'root_conditions', 'operations')
ON CONFLICT (id) DO NOTHING;
