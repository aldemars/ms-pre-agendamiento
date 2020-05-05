CREATE TABLE appointment (
     id SERIAL PRIMARY KEY,
     slot_id VARCHAR(15) UNIQUE,
     description VARCHAR(100) null,
     date DATE not null,
     hour TIME not null,
     duration INT not null DEFAULT '1',
     user_id INT not null,
     healthcare_facility_id INT not null,
     FOREIGN KEY (user_id) REFERENCES sel_user (id),
     FOREIGN KEY (healthcare_facility_id) REFERENCES healthcare_facility (id)
);
CREATE INDEX appointment_date_index ON appointment (date);