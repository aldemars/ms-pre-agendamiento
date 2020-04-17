CREATE TABLE sel_user (
  id INTEGER PRIMARY KEY IDENTITY (1,1),
  name VARCHAR(100) not null UNIQUE,
  password VARCHAR(100) not null
);