CREATE TABLE IF NOT EXISTS chat.channels (
    id uuid,
    name text,
    created_at timestamp,
    PRIMARY KEY (id, created_at)
) WITH CLUSTERING ORDER BY (created_at ASC);

CREATE TABLE IF NOT EXISTS chat.users (
    id uuid,
    provider_id text,
    email text,
    display_name text,
    avatar_url text,
    created_at timestamp,
    PRIMARY KEY (id)
);

CREATE MATERIALIZED VIEW IF NOT EXISTS chat.users_by_provider_id AS
    SELECT id, provider_id, email, display_name, avatar_url, created_at
    FROM chat.users
    WHERE provider_id IS NOT NULL
    PRIMARY KEY (provider_id);

CREATE TABLE IF NOT EXISTS chat.messages (
    id uuid,
    channel_id uuid,
    user_id uuid,
    text text,
    created_at timestamp,
    PRIMARY KEY (channel_id, created_at)
) WITH CLUSTERING ORDER BY (created_at DESC);