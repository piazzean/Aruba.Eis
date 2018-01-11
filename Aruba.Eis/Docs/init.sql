--
-- Create Standard Roles
--

BEGIN TRANSACTION;

DELETE FROM dbo.AspNetRoles;

INSERT INTO dbo.AspNetRoles(Id, Name)
VALUES ('ADMIN', 'ADMIN');

INSERT INTO dbo.AspNetRoles(Id, Name)
VALUES ('MANAGER', 'MANAGER');

INSERT INTO dbo.AspNetRoles(Id, Name)
VALUES ('AU', 'AU');

INSERT INTO dbo.AspNetRoles(Id, Name)
VALUES ('SC', 'SC');

INSERT INTO dbo.AspNetUserRoles
SELECT Id as UserId, 'ADMIN' as RoleId 
FROM dbo.AspNetUsers 
WHERE Name='Andrea Piazzesi';

INSERT INTO dbo.AspNetUserRoles
SELECT Id as UserId, 'MANAGER' as RoleId 
FROM dbo.AspNetUsers 
WHERE Name='Andrea Piazzesi';

COMMIT;