-- 3) View Query
-- Develop a view to display doctor-wise appointment summaries.

GO
CREATE or ALTER VIEW DocWiseAppmntView AS
SELECT 
    Doctors.DoctorID,
    Doctors.Name AS DoctorName,	
    Doctors.Specialty,
    COUNT(Appointments.AppointmentID) AS TotalAppointments,
    SUM(CASE WHEN Appointments.Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedAppointments
FROM Doctors
LEFT JOIN Appointments ON Doctors.DoctorID = Appointments.DoctorID
GROUP BY Doctors.DoctorID, Doctors.Name, Doctors.Specialty;
GO
select * from DocWiseAppmntView;



-- 4) Function query
-- Write a function to calculate the number of appointments per patient.

go
CREATE FUNCTION GetAppointmentsPerPatient (@PatientID INT)  
RETURNS INT  
AS  
BEGIN  
    DECLARE @AppointmentCount INT;  

    -- Assigning the count of appointments for the given PatientID
    SELECT @AppointmentCount = COUNT(*)  
    FROM Appointments  
    WHERE PatientID = @PatientID;  

    RETURN @AppointmentCount;  
END;
go

GO
CREATE FUNCTION PRINT(@INPUT VARCHAR(100))
RETURNS @Result TABLE (PrintValues VARCHAR(1))
AS
BEGIN
    DECLARE @i INT = 1;
    DECLARE @len INT = LEN(@INPUT);

    WHILE @i <= @len
    BEGIN
        INSERT INTO @Result (PrintValues)
        VALUES (SUBSTRING(@INPUT, @i, 1));
        SET @i = @i + 1;
    END

    RETURN;
END;
GO

SELECT * FROM dbo.PRINT('ABC');
SELECT dbo.GetAppointmentsPerPatient(1) AS TotalAppointments;



--5) Stored Procedure Query
--Develop a stored procedure to schedule a new appointment and update its status.

GO  
CREATE PROCEDURE ScheduleAppointment  
    @PatientID INT,  
    @DoctorID INT,  
    @AppointmentDate DATE,  
    @Status VARCHAR(50)  
AS  
BEGIN  
    SET NOCOUNT ON;

    -- Insert a new appointment
    INSERT INTO Appointments (PatientID, DoctorID, Date, Status)  
    VALUES (@PatientID, @DoctorID, @AppointmentDate, @Status);  

    -- Return the ID of the newly inserted appointment
    DECLARE @NewAppointmentID INT = SCOPE_IDENTITY();

    -- Display confirmation
    SELECT 'Appointment Scheduled Successfully' AS Message, @NewAppointmentID AS AppointmentID;  
END;  
GO  

EXEC ScheduleAppointment  
    @PatientID = 1,  
    @DoctorID = 2,  
    @AppointmentDate = '2025-03-05',  
    @Status = 'Scheduled';




--6) Join Query
-- Write JOIN queries to combine Patients, Doctors, and Appointments for comprehensive reports.

SELECT  
    p.PatientID,  
    p.Name AS PatientName,  
    p.Contact AS PatientContact,  
    d.DoctorID,  
    d.Name AS DoctorName,  
    d.Specialty,  
    a.AppointmentID,  
    a.Date AS AppointmentDate,  
    a.Status  
FROM Patients p  
JOIN Appointments a ON p.PatientID = a.PatientID  
JOIN Doctors d ON a.DoctorID = d.DoctorID  
ORDER BY a.Date DESC;




--7) Indexes
--Create indexes on PatientID, DoctorID, and Date to speed up queries.


CREATE INDEX IDX_Appointment_PatientID  
ON Appointments (PatientID);  


CREATE INDEX IDX_Appointment_DoctorID  
ON Appointments (DoctorID);  


CREATE INDEX IDX_Appointment_Date  
ON Appointments (Date);




