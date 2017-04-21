delete from Employees;
delete from shifts;
delete from ShiftsEmployees;
delete from ShiftSchedules;
SET IDENTITY_INSERT Employees ON

insert into Shifts values('SunMor','Sunday','Morning');
insert into Shifts values('SunEve','Sunday','Evening');
insert into Shifts values('SunNit','Sunday','Night');

insert into Shifts values('MonMor','Monday','Morning');
insert into Shifts values('MonEve','Monday','Evening');
insert into Shifts values('MonNit','Monday','Night');

insert into Shifts values('TueMor','Tuesday','Morning');
insert into Shifts values('TueEve','Tuesday','Evening');
insert into Shifts values('TueNit','Tuesday','Night');

insert into Shifts values('WedMor','Wednesday','Morning');
insert into Shifts values('WedEve','Wednesday','Evening');
insert into Shifts values('WedNit','Wednesday','Night');

insert into Shifts values('ThuMor','Thursday','Morning');
insert into Shifts values('ThuEve','Thursday','Evening');
insert into Shifts values('ThuNit','Thursday','Night');

insert into Shifts values('FriMor','Friday','Morning');
insert into Shifts values('FriEve','Friday','Evening');
insert into Shifts values('FriNit','Friday','Night');

insert into Shifts values('SatMor','Saturday','Morning');
insert into Shifts values('SatEve','Saturday','Evening');
insert into Shifts values('SatNit','Saturday','Night');

insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','MonNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','TueNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','MonMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','MonMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','WedMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','WedEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','WedNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','ThuMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','ThuEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','ThuNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','FriMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','FriEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','FriNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','SatMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','SatEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','SatNit');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','SunMor');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('2','SunEve');
insert into ShiftsEmployees(Employee_employeeId,Shifts_shiftId) values('1','SunNit');

insert into ShiftSchedules(shiftScheduleId, date, dayOfTheWeek, shiftType, empShiftScheduleID) values (5, '20120618 09:34:09 AM', 'Sunday', 'Morning', 5);
insert into ShiftSchedules(shiftScheduleId, date, dayOfTheWeek, shiftType, empShiftScheduleID) values (4, '20150618 11:23:12 AM', 'Monday', 'Evening', 4);
insert into ShiftSchedules(shiftScheduleId, date, dayOfTheWeek, shiftType, empShiftScheduleID) values (3, '20160618 12:34:32 AM', 'Tuesday', 'Morning', 3);
insert into ShiftSchedules(shiftScheduleId, date, dayOfTheWeek, shiftType, empShiftScheduleID) values (2, '20170718 04:34:12 AM', 'Monday', 'Evening', 2);
insert into ShiftSchedules(shiftScheduleId, date, dayOfTheWeek, shiftType, empShiftScheduleID) values (1, '20170719 11:44:06 AM', 'Tuesday', 'Evening', 1);

select * from employees;
select * from Shifts;
select * from shiftsemployees;