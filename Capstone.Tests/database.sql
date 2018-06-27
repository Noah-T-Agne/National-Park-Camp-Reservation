-- Clean the Database

DELETE FROM reservation;
DELETE FROM [site];
DELETE FROM campground;
DELETE FROM park;

-- Adding sample data to each table
SET IDENTITY_INSERT park ON;
INSERT INTO park(park_id, name, location,establish_date,area,visitors,description) VALUES (1, 'Test Park','Area 51','1776-07-04',51,1,'REDACTED');
--INSERT INTO project(project_id, name, from_date, to_date) VALUES (2, 'Test Project2', '06/25/17','06/26/17');
SET IDENTITY_INSERT park OFF;

SET IDENTITY_INSERT campground ON;
INSERT INTO campground(campground_id, park_id,name,open_from_mm,open_to_mm,daily_fee) VALUES (1,1,'Roswell',06,07,1);
SET IDENTITY_INSERT campground OFF;

SET IDENTITY_INSERT [site] ON;
INSERT INTO [site](site_id,campground_id,site_number,max_occupancy,accessible,max_rv_length,utilities) VALUES (1,1,1,1,1,1,1);
SET IDENTITY_INSERT [site] OFF;

SET IDENTITY_INSERT reservation ON;
INSERT INTO reservation(reservation_id,site_id,name,from_date,to_date,create_date) VALUES (1,1,'Groom Lake','2000-01-01','2000-01-08','1980-01-01');
SET IDENTITY_INSERT reservation OFF;

