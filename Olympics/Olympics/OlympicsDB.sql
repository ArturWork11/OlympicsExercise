CREATE DATABASE Olympics;
USE Olympics;

CREATE TABLE Athletes(id INT PRIMARY KEY ,
athleteName VARCHAR(255),
surname VARCHAR(255),
dateOfBirth DATE,
country VARCHAR(255)
);

CREATE TABLE SportEvents(id INT PRIMARY KEY ,
eventName VARCHAR (255),
eventYear INT,
eventLocation VARCHAR (255));

CREATE TABLE Competitions(id INT PRIMARY KEY ,
competitionName VARCHAR(255),
category VARCHAR(255),
isIndoor BIT,
isTeamCompetition BIT);

CREATE TABLE Medals(id INT PRIMARY KEY,
idAthlete INT NULL,
idCompetition INT NULL,
idEvent INT NULL,
medalTier VARCHAR(255),
FOREIGN KEY (idAthlete) REFERENCES Athletes(id)
ON UPDATE CASCADE 
ON DELETE SET NULL,
FOREIGN KEY (idCompetition) REFERENCES Competitions(id)
ON UPDATE CASCADE 
ON DELETE SET NULL,
FOREIGN KEY (idEvent) REFERENCES SportEvents(id)
ON UPDATE CASCADE 
ON DELETE SET NULL);

-- First request of the exercise instruction					
SELECT m.id as id , idAthlete,idCompetition,idEvent,medalTier,athleteName, surname, dateofbirth,country,competitionName, category, isIndoor, isTeamCompetition, eventName, eventYear
from medals m join athletes a
ON m.idAthlete = a.Id  
 JOIN Competitions c ON m.idCompetition = c.Id 
 JOIN SportEvents e ON m.idEvent = e.id  
 ORDER BY CASE  
 WHEN m.medalTier = 'Oro' THEN 1 
 WHEN m.medalTier = 'Argento' THEN 2
 WHEN m.medalTier = 'Bronzo' THEN 3
 END, e.eventYear;
 
 -- Second request of the exercise istructions
 SELECT idAthlete, idCompetition, idEvent, eventname, competitionName , category
 from medals m 
 join Athletes a on a.id = m.idAthlete
join SportEvents e
 on e.id = m.idEvent
 join Competitions c on m.idCompetition = c.Id
 where idEvent = 1
 order by competitionName

 -- Third request of the exercise instructions
SELECT athleteName, Surname,medalTier, count(*) as medalCount
FROM Medals m join Athletes a
ON m.idAthlete = a.id
WHERE idAthlete = 6
GROUP BY athleteName, surname, medalTier

-- fourth request of the exercise instructions
SELECT athleteName, Surname, country ,count(*) as medalCount
FROM Medals m join Athletes a
on m.idAthlete = a.id
where country = 'Indonesia'
group by athleteName, surname, country

-- fifth request of the exercise instructions
SELECT TOP 1  athleteName, surname, dateOfBirth , eventYear, MAX(DATEDIFF(YEAR,dateOfBirth,CONCAT(eventYear,'-01-01'))) AS years
FROM Medals m join Athletes a
ON a.id = m.idAthlete
JOIN SportEvents e
ON e.id = m.idEvent
WHERE medalTier = 'oro' 
group by athleteName, surname, dateOfBirth, eventYear

-- sixth request of the exercise instructions
SELECT competitionName, COUNT(distinct a.id) AS athletesInTeam, COUNT(m.id) AS medalsWon
FROM Medals m JOIN Competitions c
on m.idCompetition = c.id
JOIN Athletes a
ON m.idAthlete = a.id
WHERE isTeamCompetition = 1
group by competitionName

-- seventh request of the exercise instructions
SELECT TOP 1 category, COUNT(category) as medalsWon
FROM Competitions c join Medals m
on m.idCompetition = c.id
group by category
order by medalsWon desc