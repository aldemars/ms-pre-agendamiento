CREATE TABLE sel_user (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) not null UNIQUE,
  password VARCHAR(100) not null
);