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
    max_version INTEGER NOT NULL DEFAULT 0,
    name VARCHAR(255),
    last_change TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS public."Trigger" (
    id INTEGER PRIMARY KEY,
    id_policy INTEGER NOT NULL,
    version INTEGER NOT NULL,
    max_version INTEGER NOT NULL DEFAULT 0,
    name VARCHAR(255) NOT NULL,
    active BOOLEAN NOT NULL,
    run BOOLEAN NOT NULL,
    attack_valid BOOLEAN NOT NULL,
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
INSERT INTO public."Policy"(id, version, max_version, name)
VALUES
(1, 1, 1, 'Invasão dos Dragões'),
(2, 1, 1, 'Batalha Épica dos Magos');

INSERT INTO public."Trigger"(id, id_policy, version, max_version, name, active, run, attack_valid)
VALUES
(1, 1, 1, 1, 'Trigger de Ataque Dragão', TRUE, FALSE, TRUE),
(2, 1, 1, 1, 'Trigger de Defesa Dragão', TRUE, TRUE, FALSE);

INSERT INTO public."Trigger"(id, id_policy, version, max_version, name, active, run, attack_valid)
VALUES
(3, 2, 1, 1, 'Trigger de Magia Explosiva', TRUE, FALSE, TRUE),
(4, 2, 1, 1, 'Trigger de Escudo Mágico', TRUE, TRUE, FALSE);

-- Condition aninhada (para Trigger 1)
INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(1, 1, 1, '{"evento": "dragon_fire", "dano": 150}', NULL, NULL, NULL, NULL);

-- Condition principal que referencia a condição aninhada (para Trigger 1)
INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(2, 1, 2, '{"acao": "evasion"}', 1, 0, NULL, NULL);

INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(3, 2, 1, '{"evento": "shield_activation", "duracao": 5}', NULL, NULL, NULL, NULL),
(4, 2, 2, '{"acao": "counter_attack"}', NULL, NULL, NULL, NULL);

INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(5, 3, 1, '{"evento": "special_skill", "tempo_recarga": 10}', NULL, NULL, NULL, NULL),
(6, 3, 2, '{"acao": "boost_damage"}', NULL, NULL, NULL, NULL);

INSERT INTO public."Condition"(id, id_trigger, type, value, condition_left_id, subnode_l, condition_right_id, subnode_r)
VALUES
(7, 4, 1, '{"evento": "magic_shield", "forca": 50}', NULL, NULL, NULL, NULL),
(8, 4, 2, '{"acao": "retreat"}', NULL, NULL, NULL, NULL);