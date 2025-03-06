use BacancyPrac;
--Table for  Logging using trigger 
CREATE TABLE AppointmentLogs (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
	AppointmentID INT NOT NULL,
    OldStatus VARCHAR(50),
    NewStatus VARCHAR(50),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy SYSNAME DEFAULT SUSER_NAME(),
	FOREIGN KEY (AppointmentID) REFERENCES Appointments(AppointmentID),
   
);


--Temporary tables

CREATE TABLE #TempMedicalRecords (
    PatientID INT,
    Diagnosis NVARCHAR(255),
    Prescription NVARCHAR(255),
    CreatedDate DATETIME
);

DECLARE @TempMedicalRecords TABLE (
    PatientID INT,
    Diagnosis NVARCHAR(255),
    Prescription NVARCHAR(255),
    CreatedDate DATETIME
);

