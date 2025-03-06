use BacancyPrac;


--Trigger 
--Implement a trigger to log any changes to appointment statuses.
CREATE TRIGGER trg_AppointmentStatusChange
ON Appointments
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AppointmentLogs (AppointmentID, OldStatus, NewStatus, ModifiedDate, ModifiedBy)
    SELECT 
        i.AppointmentID, 
        d.Status AS OldStatus, 
        i.Status AS NewStatus, 
        GETDATE(), 
        SUSER_NAME()
    FROM inserted i
    INNER JOIN deleted d ON i.AppointmentID = d.AppointmentID
    WHERE i.Status <> d.Status;  -- Only log changes when status is updated
END;

UPDATE Appointments
SET Status = 'Completed'
WHERE AppointmentID = 9;

select * from Appointments;
select * from AppointmentLogs;




---- Cursor 
--Use a cursor to iterate through patient records and identify those with repeated visits.
DECLARE @PatientID INT, @VisitCount INT;

DECLARE PatientCursor CURSOR FOR
SELECT PatientID, COUNT(AppointmentID) AS VisitCount
FROM Appointments
GROUP BY PatientID
HAVING COUNT(AppointmentID) > 1;  -- Only patients with multiple visits

OPEN PatientCursor;

FETCH NEXT FROM PatientCursor INTO @PatientID, @VisitCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    PRINT 'Patient ID: ' + CAST(@PatientID AS VARCHAR) + ' has ' + CAST(@VisitCount AS VARCHAR) + ' visits.';

    FETCH NEXT FROM PatientCursor INTO @PatientID, @VisitCount;
END;

CLOSE PatientCursor;
DEALLOCATE PatientCursor;



----Temporary tables
--Store patient diagnostic data temporarily for analytical purposes.
INSERT INTO #TempMedicalRecords (PatientID, Diagnosis, Prescription, CreatedDate)
SELECT PatientID, Diagnosis, Prescription, CreatedDate
FROM MedicalRecords;

-- Perform analysis (Example: Count diagnoses per patient)
SELECT PatientID, COUNT(Diagnosis) AS DiagnosisCount
FROM #TempMedicalRecords
GROUP BY PatientID;

-- Drop the temporary table once analysis is done
DROP TABLE #TempMedicalRecords;


--ALternate way
-- Insert data from the main MedicalRecords table
INSERT INTO @TempMedicalRecords (PatientID, Diagnosis, Prescription, CreatedDate)
SELECT PatientID, Diagnosis, Prescription, CreatedDate
FROM MedicalRecords;

-- Perform analysis
SELECT PatientID, COUNT(Diagnosis) AS DiagnosisCount
FROM @TempMedicalRecords
GROUP BY PatientID;




-- CTE
-- Use a CTE to retrieve all active doctors along with their patient count.
WITH DoctorPatientCount AS (
    SELECT 
        d.DoctorID,
        d.Name AS DoctorName,
        d.Specialty,
        COUNT(a.PatientID) AS PatientCount
    FROM Doctors d
    LEFT JOIN Appointments a ON d.DoctorID = a.DoctorID
    GROUP BY d.DoctorID, d.Name, d.Specialty
)
SELECT * FROM DoctorPatientCount;



--Constraints
-- Add a UNIQUE constraint to ensure no duplicate patient records are entered.
Alter table patients 
add constraint UQ_Patient UNIQUE (Name, DOB, Contact);
