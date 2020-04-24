CREATE TABLE sel_user (
  id INTEGER PRIMARY KEY AUTOINCREMENT,
  name VARCHAR(100) not null UNIQUE,
  password VARCHAR(100) not null,
  role VARCHAR(100) not null default 'scheduler'
);

INSERT INTO sel_user (name,password) VALUES("name","password");