use BacancyPrac;

CREATE TABLE Patients (
    PatientID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    DOB DATE NOT NULL,
    Contact NVARCHAR(15) UNIQUE NOT NULL,
    Address NVARCHAR(500),
    Gender CHAR(1) CHECK (Gender IN ('M', 'F', 'O')) NOT NULL
);

CREATE TABLE Doctors (
    DoctorID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Specialty NVARCHAR(255) NOT NULL,
    Contact NVARCHAR(15) UNIQUE NOT NULL,
    Department NVARCHAR(255) NOT NULL
);

CREATE TABLE Appointments (
    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    DoctorID INT NOT NULL,
    Date DATETIME NOT NULL,
    Status NVARCHAR(50) CHECK (Status IN ('Scheduled', 'Completed', 'Cancelled')) NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID) ON DELETE CASCADE,
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID) ON DELETE CASCADE
);

CREATE TABLE MedicalRecords (
    RecordID INT IDENTITY(1,1) PRIMARY KEY,
    PatientID INT NOT NULL,
    Diagnosis NVARCHAR(500) NOT NULL,
    Prescription NVARCHAR(1000) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID) ON DELETE CASCADE
);

INSERT INTO Patients (Name, DOB, Contact, Address, Gender) VALUES  
('John Doe', '1985-06-15', '987650001', '123 Main St, NY', 'M'),  
('Jane Smith', '1990-09-25', '987650002', '456 Elm St, CA', 'F'),  
('Mike Johnson', '1978-03-12', '987650003', '789 Oak St, TX', 'M'),  
('Emily Davis', '2000-07-08', '987650004', '321 Pine St, FL', 'F'),  
('David Wilson', '1995-11-30', '987650005', '654 Maple St, IL', 'M'),  
('Sophia Martinez', '1982-01-14', '987650006', '987 Birch St, AZ', 'F'),  
('Daniel Brown', '1975-04-22', '987650007', '159 Cedar St, WA', 'M'),  
('Olivia Taylor', '1988-08-10', '987650008', '753 Spruce St, NV', 'F'),  
('James Anderson', '1992-12-05', '987650009', '852 Redwood St, CO', 'M'),  
('Emma White', '1998-06-18', '987650010', '369 Walnut St, OR', 'F');

INSERT INTO Doctors (Name, Specialty, Contact, Department) VALUES  
('Dr. Adams', 'Cardiology', '876540001', 'Heart Care'),  
('Dr. Baker', 'Neurology', '876540002', 'Brain & Nerves'),  
('Dr. Carter', 'Orthopedics', '876540003', 'Bone & Joints'),  
('Dr. Davis', 'Pediatrics', '876540004', 'Child Care'),  
('Dr. Evans', 'Dermatology', '876540005', 'Skin & Hair'),  
('Dr. Foster', 'Gastroenterology', '876540006', 'Digestive System'),  
('Dr. Garcia', 'Psychiatry', '876540007', 'Mental Health'),  
('Dr. Harris', 'Endocrinology', '876540008', 'Hormones & Diabetes'),  
('Dr. Johnson', 'Ophthalmology', '876540009', 'Eye Care'),  
('Dr. King', 'Pulmonology', '876540010', 'Lung & Respiratory');


INSERT INTO Appointments (PatientID, DoctorID, Date, Status) VALUES  
(1, 1, '2025-03-10 10:00:00', 'Scheduled'),  
(2, 2, '2025-03-11 11:30:00', 'Completed'),  
(3, 3, '2025-03-12 14:00:00', 'Scheduled'),  
(4, 4, '2025-03-13 16:30:00', 'Cancelled'),  
(5, 5, '2025-03-14 09:00:00', 'Scheduled'),  
(6, 6, '2025-03-15 13:45:00', 'Completed'),  
(7, 7, '2025-03-16 15:00:00', 'Scheduled'),  
(8, 8, '2025-03-17 10:15:00', 'Completed'),  
(9, 9, '2025-03-18 12:30:00', 'Scheduled'),  
(10, 10, '2025-03-19 14:45:00', 'Cancelled');


INSERT INTO MedicalRecords (PatientID, Diagnosis, Prescription, CreatedDate) VALUES
(1, 'Hypertension', 'Lisinopril 10mg daily', '2024-02-20 10:30:00'),
(2, 'Diabetes Type 2', 'Metformin 500mg twice daily', '2024-02-21 11:15:00'),
(3, 'Asthma', 'Albuterol inhaler as needed', '2024-02-22 09:45:00'),
(4, 'Migraine', 'Sumatriptan 50mg as needed', '2024-02-23 14:00:00'),
(5, 'Allergic Rhinitis', 'Loratadine 10mg daily', '2024-02-24 08:20:00'),
(6, 'Hyperthyroidism', 'Methimazole 5mg daily', '2024-02-25 13:10:00'),
(7, 'Gastritis', 'Omeprazole 20mg daily', '2024-02-26 12:35:00'),
(8, 'Osteoarthritis', 'Ibuprofen 400mg as needed', '2024-02-27 15:25:00'),
(9, 'Anemia', 'Ferrous sulfate 325mg daily', '2024-02-28 09:50:00'),
(10, 'Depression', 'Sertraline 50mg daily', '2024-02-29 17:10:00');
