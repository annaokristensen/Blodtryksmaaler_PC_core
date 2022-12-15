CREATE TABLE MeasurementData_DB (
measurementID int primary key,
cpr nVARCHAR (11) foreign key references CPR_table(cpr),
rawData varbinary(MAX),
systoliskValues varbinary(MAX),
diastoliskValues varbinary(MAX),
pulseValues varbinary(MAX),
middelValues varbinary(MAX),
startDateTime DATETIME,
stopDateTime DATETIME,
alarmDateTimes varbinary(MAX),
);