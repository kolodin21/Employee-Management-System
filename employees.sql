CREATE SCHEMA test;

CREATE TABLE table_persons(
  id SERIAL NOT NULL PRIMARY KEY,
  name TEXT NOT NULL,
  sur_name TEXT NOT NULL,
  patronymic TEXT
);

CREATE TABLE table_positions(
    id SERIAL NOT NULL PRIMARY KEY,
    position_name TEXT NOT NULL
);

CREATE TABLE table_departments(
    id SERIAL NOT NULL PRIMARY KEY,
    department_name TEXT NOT NULL
);

CREATE TYPE leave_types_enum AS ENUM ('vacation','medical','dismissed');

CREATE TABLE table_leaves(
    id BIGSERIAL NOT NULL PRIMARY KEY,
    leave_type leave_types_enum NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE
);

CREATE TABLE table_employees(
    id SERIAL NOT NULL PRIMARY KEY,
    person_id INT NOT NULL,
    department_id INT NOT NULL,
    position_id INT NOT NULL,
    hire_date DATE NOT NULL,
    date_of_dismissal DATE,
    FOREIGN KEY (person_id) REFERENCES table_persons(id),
    FOREIGN KEY (department_id) REFERENCES table_departments(id),
    FOREIGN KEY (position_id) REFERENCES table_positions(id)
);

CREATE TABLE table_employees_leave(
    id SERIAL NOT NULL PRIMARY KEY,
    employee_id INT NOT NULL,
    leave_id INT NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES table_employees(id),
    FOREIGN KEY (leave_id) REFERENCES table_leaves(id)
);

CREATE VIEW view_employees AS
SELECT table_persons.id AS person_id,
       table_persons.name AS name,
       table_persons.sur_name AS sur_name,
       table_persons.patronymic AS patronymic,
       table_departments.id AS department_id,
       table_departments.department_name AS department_name,
       table_positions.id AS position_id,
       table_positions.position_name AS position_name,
       table_employees.hire_date AS hire_date,
       table_employees.date_of_dismissal AS dismissal_date
FROM table_employees
JOIN table_persons ON table_employees.person_id = table_persons.id
JOIN table_departments ON table_employees.department_id = table_departments.id
JOIN table_positions ON table_employees.position_id = table_positions.id;

CREATE VIEW view_employees_leave AS
SELECT table_persons.id AS person_id,
       table_persons.name AS name,
       table_persons.sur_name AS sur_name,
       table_persons.patronymic AS patronymic,
       table_departments.id AS department_id,
       table_departments.department_name AS department_name,
       table_positions.id AS position_id,
       table_positions.position_name AS position_name,
       table_leaves.leave_type AS leave_type,
       table_leaves.start_date as begin_date,
       table_leaves.end_date AS end_date
FROM table_employees_leave
JOIN table_leaves ON table_employees_leave.leave_id = table_leaves.id
JOIN table_employees ON table_employees_leave.employee_id = table_employees.id
JOIN table_persons ON table_employees.person_id = table_persons.id
JOIN table_departments ON table_employees.department_id = table_departments.id
JOIN table_positions ON table_employees.position_id = table_positions.id;