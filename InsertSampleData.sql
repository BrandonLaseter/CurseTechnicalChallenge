

--Insert some teams (without avatar images)
SET IDENTITY_INSERT dbo.Teams ON;
INSERT INTO dbo.Teams(ID, Name) Values(1, 'Atlanta Braves');
INSERT INTO dbo.Teams(ID, Name) Values(2, 'New York Mets');
INSERT INTO dbo.Teams(ID, Name) Values(3, 'San Francisco Giants');
INSERT INTO dbo.Teams(ID, Name) Values(4, 'Florida Marlins');
INSERT INTO dbo.Teams(ID, Name) Values(5, 'Colorado Rockies');
SET IDENTITY_INSERT dbo.Teams OFF;

--Insert some sample players. They will still need to be assigned teams and avatars.
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Chipper Jones', 300, 7, 400, 1) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 1', 24, 1, 230, 1) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 2', 50, 3, 400, 1) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 3', 4, 0, 120, 2) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 4', 50, 4, 500, 3) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 5', 10, 0, 100, 3) ;
INSERT INTO dbo.Players (Name, RBI, HomeRun, Average, TeamId) Values ('Player 6', 200, 15, 550, 4) ;