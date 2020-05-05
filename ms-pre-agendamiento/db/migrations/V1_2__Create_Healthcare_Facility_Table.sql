CREATE TABLE healthcare_facility (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) not null,
  address VARCHAR(100) null,
  working_hours_from TIME not null, 
  working_hours_to TIME not null
);