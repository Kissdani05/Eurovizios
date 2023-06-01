CREATE DATABASE IF NOT EXISTS eurovizio CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE eurovizio;


CREATE TABLE IF NOT EXISTS dal (
  id INT PRIMARY KEY AUTO_INCREMENT,
  ev INT,
  sorrend INT,
  orszag VARCHAR(255),
  eloado VARCHAR(255),
  cim VARCHAR(255),
  helyezes INT,
  pontszam INT
);


CREATE TABLE IF NOT EXISTS verseny (
  ev INT PRIMARY KEY,
  datum DATE,
  varos VARCHAR(255),
  orszag VARCHAR(255),
  induloszam INT
);




INSERT INTO dal (ev, sorrend, orszag, eloado, cim, helyezes, pontszam) VALUES
(2020, 1, 'Magyarország', 'Előadó 1', 'Dal 1', 2, 150),
(2020, 2, 'Németország', 'Előadó 2', 'Dal 2', 5, 100),
(2021, 1, 'Magyarország', 'Előadó 3', 'Dal 3', 1, 200),
(2021, 2, 'Németország', 'Előadó 4', 'Dal 4', 3, 180);


INSERT INTO verseny (ev, datum, varos, orszag, induloszam) VALUES
(2020, '2020-05-18', 'Budapest', 'Magyarország', 2),
(2021, '2021-06-05', 'Berlin', 'Németország', 2);
