use s31_Data_Polling_JDF;

create table Machine (
id Integer unique auto_increment Primary Key,
type varchar(40),
yearBuilt Integer
);

create table User(
id Integer unique auto_increment Primary Key,
surname varchar(20),
lastname varchar(20),
username varchar(20),
password varchar(20)
);

create table Job(
id Integer unique auto_increment Primary Key, 
jdfJobId Integer,
xmlPath varchar(40),
status varchar(30)
);

create table UserJobs(
id Integer Unique auto_increment Primary Key,
userid Integer,
jobId Integer,
foreign key(userId) References User(id)
on delete cascade
on update cascade,
foreign key(jobId) References Job(id)
on delete cascade
on update cascade
);

create table MachineJobs(
id Integer unique auto_increment Primary Key,
machineId Integer, 
jobId Integer,
foreign key(machineId) References Machine(id)
on delete cascade
on update cascade,
foreign key(jobId) References Job(id))