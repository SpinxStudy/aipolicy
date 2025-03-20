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

CREATE TABLE IF NOT EXISTS public."Policy" (
    id INTEGER PRIMARY KEY,
    version INTEGER NOT NULL,
    name VARCHAR(255),
    last_change TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS public."Trigger" (
    id INTEGER PRIMARY KEY,
    id_policy INTEGER NOT NULL,
    version INTEGER NOT NULL,
    name VARCHAR(255) NOT NULL,
    active BOOLEAN NOT NULL,
    run BOOLEAN NOT NULL,
    attack_valid BOOLEAN NOT NULL,
    last_change TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_policy FOREIGN KEY (id_policy)
        REFERENCES public."Policy"(id)
);

CREATE TABLE IF NOT EXISTS public."Condition" (
    id INTEGER PRIMARY KEY,
    id_trigger INTEGER NOT NULL,
    type INTEGER NOT NULL,
    value JSONB,
    condition_left_id INTEGER,
    subnode_l INTEGER,
    condition_right_id INTEGER,
    subnode_r INTEGER,
    last_change TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_trigger FOREIGN KEY (id_trigger)
        REFERENCES public."Trigger"(id),
    CONSTRAINT fk_condition_left FOREIGN KEY (condition_left_id)
        REFERENCES public."Condition"(id)
        ON DELETE SET NULL,
    CONSTRAINT fk_condition_right FOREIGN KEY (condition_right_id)
        REFERENCES public."Condition"(id)
        ON DELETE SET NULL
);

-- Concede privilégios para ao app_user
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO app_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON TABLES TO app_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL PRIVILEGES ON SEQUENCES TO app_user;

-- Mocking de dados
INSERT INTO public."Policy"(id, version, name)
VALUES
(1, 1, 'Evento Perfect World - Invasão');

INSERT INTO public."Trigger"(id, id_policy, version, name, active, run, attack_valid)
VALUES
(1, 1, 1, 'Trigger de Ataque dos Dragões', TRUE, FALSE, TRUE),
(2, 1, 1, 'Trigger de Defesa dos Dragões', TRUE, TRUE, FALSE);

INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(1, 1, 1, '{"evento": "dragon_fire", "dano": "150"}', NULL, NULL, NULL, NULL),
(2, 1, 2, '{"acao": "evasion"}', 1, 0, NULL, NULL);

INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(3, 2, 1, '{"evento": "shield_activation", "duracao": "5"}', NULL, NULL, NULL, NULL),
(4, 2, 2, '{"acao": "counter_attack"}', NULL, NULL, NULL, NULL);