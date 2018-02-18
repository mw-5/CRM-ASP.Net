/* Create database with command:
CREATE DATABASE crm_db;
afterwards 
1. select created database node in object explorer 
2. open sql query editor 
3. execute this script  */

CREATE TABLE customers
(
  cid integer NOT NULL PRIMARY KEY,
  company character varying(255),
  address character varying(255),
  zip character varying(255),
  city character varying(255),
  country character varying(255),
  contract_id character varying(255),
  contract_date timestamp without time zone
);

CREATE TABLE contact_persons
(
  id serial NOT NULL PRIMARY KEY,
  cid integer NOT NULL REFERENCES customers(cid),
  forename character varying(255),
  surname character varying(255),
  gender character(1) CHECK (gender = 'm' OR gender = 'f'),
  email character varying(255),
  phone character varying(255),
  main_contact boolean DEFAULT false
);

CREATE TABLE notes
(
  id integer NOT NULL PRIMARY KEY,
  cid integer NOT NULL REFERENCES customers(cid),
  created_by character varying(255) NOT NULL,
  entry_date timestamp without time zone NOT NULL,
  memo text,
  category character varying(255),
  attachment text
);

CREATE OR REPLACE FUNCTION func_main_contact() RETURNS trigger AS $result$
BEGIN
  IF NEW.main_contact = true THEN
	UPDATE contact_persons SET main_contact = false WHERE cid = NEW.cid AND id <> NEW.id;
	RETURN NEW;
  END IF;
END;
$result$ LANGUAGE plpgsql;

CREATE TRIGGER main_contact
AFTER INSERT OR UPDATE OF main_contact ON contact_persons
FOR EACH ROW
WHEN (NEW.main_contact = true)
EXECUTE PROCEDURE func_main_contact();	

CREATE GROUP crm_users;
GRANT ALL ON customers TO crm_users;
GRANT ALL ON contact_persons TO crm_users;
GRANT ALL ON notes TO crm_users;
CREATE USER crm_user WITH PASSWORD '123';
GRANT crm_users TO crm_user;

INSERT INTO customers(cid, company, address, zip, city, country, contract_id, contract_date) 
VALUES (1, 'Smoothies Gmbh', 'Torstr. 35', '10119', 'Berlin', 'Germany', 'g1', timestamp '2017-02-01 00:00:00'),
(2, 'StartUp Coffee, Inc.', '2675 Middlefield Rd A', 'CA 94306', 'Palo Alto', 'USA', 'u1', timestamp '2017-03-04 00:00:00'),
(3, 'El Sabor del Cacao S.L.', 'Plaza Puerta del Sol 6', '28013', 'Madrid', 'Spain', 's1', timestamp '2017-04-02 00:00:00');

INSERT INTO contact_persons(id, cid, forename, surname, gender, email, phone, main_contact) 
VALUES(1, 1, 'Chantal', 'Obst', 'f', 'chantal.obst@smoothies.de', '+49 30 12345678', true),
(2, 2, 'Mike', 'Miller','m', 'mike.miller@startupcafe.com', '+1 650 1234567', true),
(3, 3, 'Carlos', 'Sanchez', 'm', 'carlos.sanchez@sabordelcacao.es', '+34 915 123456', true),
(4, 3, 'Carla', 'Sanchez', 'f', 'carla.sanchez@sabordelcacao.es', '+34 915 123456', false);

