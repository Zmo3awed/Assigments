--Question 1 — Commit a Patient Update
--Start a transaction and update the ward of patient Id = 1 to ward 2.
--Commit the transaction so that the change is permanently saved.


BEGIN TRAN 

UPDATE Patients
SET WardId = 2
WHERE WARDID = 1

COMMIT TRAN;
SELECT *
FROM PATIENTS


--Question 2 — Rollback a Consultant Salary Update
--Start a transaction and increase the salary of consultant Id = 1 by 5000.
--Then rollback the transaction so that the salary returns to its original value.

BEGIN TRAN 
SELECT *
FROM Consultants
WHERE ID =301

UPDATE Consultants
SET Salary +=5000
WHERE ID =301

SELECT *
FROM Consultants
WHERE ID =301

ROLLBACK TRAN

SELECT *
FROM Consultants
WHERE ID =301



--Question 3 — Commit Multiple Related Updates
--Start one transaction that:
--Moves patient Id = 1 to ward 3.
--Updates nurse Number = 1 so that the nurse serves ward 3.
--Commit the transaction after both operations are completed successfully.


BEGIN TRANSACTION 
UPDATE Patients
SET WardId=3
WHERE ID = 1

UPDATE Nurses
SET ServesInWardId = 3
WHERE Number = 201

IF(@@TRANCOUNT >0)
   ROLLBACK TRAN
ELSE
COMMIT TRANSACTION
--Question 4 — Use a Savepoint
--Start a transaction that:
--Updates patient Id = 1 and assigns the patient to ward 2.
--Creates a savepoint named PatientUpdate.
--Changes the patient's name to 'Temporary Patient Name'.
--Rolls back only to the PatientUpdate savepoint.
--Commits the transaction.
--The ward change should remain, while the temporary name change should be cancelled.



BEGIN TRAN 

UPDATE Patients
SET WardId = 2
WHERE ID = 1

SAVE TRAN PatientUpdate

UPDATE Patients
SET NAME = 'Temporary Patient Name'
WHERE ID = 1
ROLLBACK TRAN PatientUpdate
COMMIT TRAN;
SELECT *
FROM PATIENTS



--Question 5 — Transaction with TRY/CATCH
--Create a transaction that increases the salary of consultant Id = 1 by 3000.
--Use TRY...CATCH.
--If the operation succeeds, commit the transaction.
--If an error occurs, rollback the transaction and display:
--Error number
--Error message

BEGIN TRY
    BEGIN TRANSACTION;

    UPDATE Consultants
    SET SALARY += 3000 
    WHERE Id = 201;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
]
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO


-------------------------------------------------------- VIEWS ------------------------------------



--Question 6 — Consultant Patient Statistics
--Create vw_ConsultantPatientStatistics displaying Consultant ID, Consultant Name, Salary, and number of assigned patients. Use Consultants, PatientConsultantAssignments, COUNT(), and GROUP BY.

 GO
CREATE OR ALTER VIEW vw_ConsultantPatientStatistics 
AS
SELECT ID ,NAME ,Salary ,COUNT(PC.PatientId) AS PATIENTSNUM
FROM Consultants C JOIN 
PatientConsultantAssignments PC
ON C.Id = PC.ConsultantId
GROUP BY ID ,NAME ,SALARY
GO

SELECT *
FROM vw_ConsultantPatientStatistics

--Question 7 — Find Consultants with More Than 5 Patients
--Using vw_ConsultantPatientStatistics, display consultants who have more than 5 assigned patients.


SELECT *
FROM vw_ConsultantPatientStatistics
WHERE PATIENTSNUM > 5


--Question 8 — Create an Indexed Aggregate View
--Create dbo.vw_Ind exedConsultantPatientCount displaying Consultant ID and number of assigned patients.
--Use COUNT_BIG(), GROUP BY, WITH SCHEMABINDING, and then create the required unique clustered index.
GO
CREATE OR ALTER VIEW dbo.VW_IndexedConsultant_PatientASsignedCount
WITH SCHEMABINDING , ENCRYPTION
AS
SELECT
    ConsultantId,
    COUNT_BIG(*) AS PatientCount
FROM dbo.PatientConsultantAssignments
GROUP BY ConsultantId;
GO

CREATE UNIQUE CLUSTERED INDEX IX_VIEW
ON VW_IndexedConsultant_PatientASsignedCount(ConsultantId)

--Question 9 — Query the Indexed Aggregate View
--Display consultants whose number of assigned patients is greater than 5.

SELECT *
FROM VW_IndexedConsultant_PatientASsignedCount
WHERE PatientCount > 5

--Question 10 — Display All Views
--Display View Name, Creation Date, and Modification Date for all views in the Hospital database.


SELECT
    name AS ViewName,
    create_date AS CreationDate,
    modify_date AS ModificationDate
FROM sys.views
ORDER BY name;

--Question 11 — Display a View Definition
--Display the SQL definition of vw_PatientInformation.


SELECT OBJECT_DEFINITION(
    OBJECT_ID('DBO.vw_ConsultantPatientStatistics ')
) AS ViewDefinition;
--Question 12 — Alter a View
--Modify vw_PatientInformation so that it returns only Patient ID, Patient Name, and Date of Birth.
GO
CREATE OR ALTER VIEW vw_PatientInformation
WITH ENCRYPTION 
AS
SELECT ID ,NAME ,DOB
FROM Patients

--Question 13 — Drop a View
--Remove vw_ConsultantSalary.
DROP VIEW vw_ConsultantSalary.

--Question 14 — Patient Hospital Report
--Create vw_PatientHospitalReport displaying Patient ID, Patient Name, DOB, Ward Name, and Consultant Name.
--Use Patients, Wards, PatientConsultantAssignments, and Consultants.
--Use LEFT JOIN so patients remain visible even without a ward or consultant.
GO
CREATE OR ALTER VIEW PATIENTS_HOSPITAL_REBORT
WITH ENCRYPTION 
AS
SELECT P.Id AS PATIENT_ID,P.Name AS PATIENT_NAME ,P.DOB 
, W.Name AS WARD_NAME,
C.Name AS CONSULTANT_NAME
FROM PATIENTS P LEFT JOIN WARDS W
ON P.WardId = W.Id
LEFT JOIN PatientConsultantAssignments PC
ON P.Id = PC.PatientId
LEFT JOIN Consultants C
ON C.Id = PC.ConsultantId 

SELECT *
FROM PATIENTS_HOSPITAL_REBORT

--Question 15 — Search the Hospital Report
--Using vw_PatientHospitalReport, display only patients whose consultant name is not NULL.

SELECT *
FROM PATIENTS_HOSPITAL_REBORT
WHERE CONSULTANT_NAME IS NOT NULL

--Question 16 — Display Patients by Ward
--Using vw_PatientHospitalReport, display Patient Name, Ward Name,
--and Consultant Name for patients belonging to a specific ward.

SELECT PATIENT_NAME ,WARD_NAME ,CONSULTANT_NAME
FROM PATIENTS_HOSPITAL_REBORT
WHERE WARD_NAME = 'Intensive Care Unit (ICU)'



--Question 17 — Most Patients per Consultant
--Using vw_ConsultantPatientStatistics,
--display the consultant with the highest number of assigned patients.

SELECT *
FROM vw_ConsultantPatientStatistics
WHERE PATIENTSNUM = 
(SELECT MAX(PATIENTSNUM)
FROM vw_ConsultantPatientStatistics 
)

--Question 18 — View + Filtering
--Using vw_PatientAgeInformation, display patients whose age is greater than 60.
GO
CREATE OR ALTER  VIEW vw_PatientAgeInformation
WITH ENCRYPTION 
AS 
SELECT NAME ,DATEDIFF(YEAR, DOB, GETDATE()) AS Age
FROM Patients;

SELECT *
FROM vw_PatientAgeInformation
WHERE AGE>60


--Question 19 — View + Sorting
--Using vw_ConsultantPatientStatistics, 
--display all consultants ordered from the consultant with the highest number of patients to the lowest.

SELECT *
FROM vw_ConsultantPatientStatistics
ORDER BY PATIENTSNUM DESC

--Question 20 — View + Aggregation
--Using vw_PatientWardInformation, display the number of patients in each ward.


SELECT WARD_NAME ,COUNT(PATIENT_ID) 'NUM OF PATIENTS'
FROM PATIENTS_HOSPITAL_REBORT
GROUP BY WARD_NAME 


--Question 21 — Find Indexed Views
--Display all views that have indexes. Show Schema Name, View Name, Index Name, and Index Type.
SELECT
    S.name AS SchemaName,
    V.name AS ViewName,
    I.name AS IndexName,
    I.type_desc AS IndexType
FROM sys.views V
JOIN sys.schemas S
    ON V.schema_id = S.schema_id
JOIN sys.indexes I
    ON V.object_id = I.object_id
WHERE I.index_id > 0
ORDER BY S.name, V.name;
