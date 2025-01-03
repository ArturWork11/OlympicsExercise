using Microsoft.Identity.Client;
using MSSTU.DB.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olympics
{
    internal class Menu
    {
        #region Properties
        DAOAthletes daoAthletes = DAOAthletes.GetInstance();
        DAOCompetitions daoCompetitions = DAOCompetitions.GetInstance();
        DAOEvents daoEvents = DAOEvents.GetInstance();
        DAOMedals daoMedals = DAOMedals.GetInstance();
        #endregion

        #region Menu
        public void MenuMethods()
        {
            do
            {

                Console.Clear();
                Console.WriteLine("1.  Import from file");
                Console.WriteLine("2.  Insert Athlete");
                Console.WriteLine("3.  Update Athlete");
                Console.WriteLine("4.  Delete Athlete");
                Console.WriteLine("5.  Found Athlete");
                Console.WriteLine("6.  Show All Athletes");
                Console.WriteLine("7.  Insert Event");
                Console.WriteLine("8.  Update Event");
                Console.WriteLine("9.  Delete Event");
                Console.WriteLine("10. Found Event");
                Console.WriteLine("11. Show All Events");
                Console.WriteLine("12. Insert Competition");
                Console.WriteLine("13. Update Competition");
                Console.WriteLine("14. Delete Competition");
                Console.WriteLine("15. Found Competition");
                Console.WriteLine("16. Show All Competition");
                Console.WriteLine("17. Insert Medal");
                Console.WriteLine("18. Update Medal");
                Console.WriteLine("19. Delete Medal");
                Console.WriteLine("20. Found Medal");
                Console.WriteLine("21. Show All Medals");
                Console.WriteLine("22. Show All Medals of an Athlete");
                Console.WriteLine("23. Show All the Competitions of an Event");
                Console.WriteLine("24. Show the count of each tier of medals won by an athlete");
                Console.WriteLine("25. Show all the athletes who won medals for their country");
                Console.WriteLine("26. Show the oldest athlete who won a gold medal");
                Console.WriteLine("27. Show how many medals were won in team competitions and how many athletes participated in each competition");
                Console.WriteLine("28. Show the category that won the most medals");
                Console.WriteLine("0.  Exit");
                Console.WriteLine("Choose an option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ImportFromFileMethods();
                        break;
                    case "2":
                        InsertAthlete();
                        break;
                    case "3":
                        UpdateAthlete();
                        break;
                    case "4":
                        DeleteAthlete();
                        break;
                    case "5":
                        FoundAthlete();
                        break;
                    case "6":
                        ShowAllAthlete();
                        break;
                    case "7":
                        InsertEvent();
                        break;
                    case "8":
                        UpdateEvent();
                        break;
                    case "9":
                        DeleteEvent();
                        break;
                    case "10":
                        FoundEvent();
                        break;
                    case "11":
                        ShowAllEvent();
                        break;
                    case "12":
                        InsertCompetition();
                        break;
                    case "13":
                        UpdateCompetition();
                        break;
                    case "14":
                        DeleteCompetition();
                        break;
                    case "15":
                        FoundCompetition();
                        break;
                    case "16":
                        ShowAllCompetition();
                        break;
                    case "17":
                        InsertMedal();
                        break;
                    case "18":
                        UpdateMedal();
                        break;
                    case "19":
                        DeleteMedal();
                        break;
                    case "20":
                        FoundMedal();
                        break;
                    case "21":
                        ShowAllMedals();
                        break;
                    case "22":
                        Console.WriteLine("Insert athlete id: ");
                        int id = int.Parse(Console.ReadLine());
                        daoMedals.AllMedalsOfAnAthlete(id);
                        break;
                    case "23":
                        Console.WriteLine("Insert event id: ");
                        int eventId = int.Parse(Console.ReadLine());
                        daoCompetitions.AllCompetitionsOfAnEvent(eventId);
                        break;
                    case "24":
                        Console.WriteLine("Insert athlete id: ");
                        int athleteId = int.Parse(Console.ReadLine());
                        daoMedals.CountOfEachTierOfMedalsWonByAnAthlete(athleteId);
                        break;
                    case "25":
                        Console.WriteLine("Insert country: ");
                        string country = Console.ReadLine();
                        daoMedals.AthletesWhoWonMedalsForTheirCountry(country);
                        break;
                    case "26":
                        daoMedals.OldestAthleteWhoWonAGoldMedal();
                        break;
                    case "27":
                        daoMedals.MedalsWonInTeamCompetitionsAndAthletes();
                        break;
                    case "28":
                        daoMedals.CategoryThatWonTheMostMedals();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            } while (true);
        }

        #endregion

        #region Methods
        public void ImportFromFileMethods()
        {

            string baseFilePath = "../../../Exercise Material/File Dati/";

            string medalsFilePath = baseFilePath +  "Medagliere.txt";
            string athletesFilePath = baseFilePath + "Atleti.txt";
            string competitionsFilePath = baseFilePath + "Gare.txt";
            string eventsFilePath = baseFilePath + "Eventi.txt";
            if (daoAthletes.GetRecords() == null)
            {
                daoAthletes.ImportAthletesFromFile(athletesFilePath);
                Console.WriteLine("Athletes imported successfully");
            }
            else
            {
                Console.WriteLine("Athletes already imported");
            }
            if (daoCompetitions.GetRecords() == null)
            {
                daoCompetitions.ImportCompetitionsFromFile(competitionsFilePath);
                Console.WriteLine("Competitions imported successfully");
            }
            else
            {
                Console.WriteLine("Competitions already imported");
            }
            if (daoEvents.GetRecords() == null)
            {
                daoEvents.ImportEventsFromFile(eventsFilePath);
                Console.WriteLine("Events imported successfully");
            }
            else
            {
                Console.WriteLine("Events already imported");
            }
            if (daoMedals.GetRecords() == null)
            {
                daoMedals.ImportMedalsFromFile(medalsFilePath);
                Console.WriteLine("Medals imported successfully");
            }
            else
            {
                Console.WriteLine("Medals already imported");
            }

        }
        public void InsertAthlete()
        {
            Athlete athlete = new Athlete();
            bool newId = false;
            Console.WriteLine("Insert athlete id: ");
            do
            {
                athlete.Id = int.Parse(Console.ReadLine());
                if (daoAthletes.FindRecord(athlete.Id) != null)
                {
                    Console.WriteLine("Athlete already exists, \nPlease insert a new id");
                    newId = true;
                }
                else
                {
                    newId = false;
                }
            } while (newId != false);
            Console.WriteLine("Insert athlete name: ");
            athlete.AthleteName = Console.ReadLine();
            Console.WriteLine("Insert athlete surname: ");
            athlete.Surname = Console.ReadLine();
            Console.WriteLine("Insert athlete date of birth: ");
            bool rightDate = false;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    athlete.DateOfBirth = dateOfBirth;
                    rightDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format \nPlease insert a valid format like 1999-12-12");
                    rightDate = false;
                }
            } while (rightDate != true);
            Console.WriteLine("Insert athlete country: ");
            athlete.Country = Console.ReadLine();
            if (daoAthletes.CreateRecord(athlete))
            {
                Console.WriteLine("Athlete inserted successfully");
            }
            else
            {
                Console.WriteLine("Failed to insert athlete");
            }
        }
        public void UpdateAthlete()
        {
            Athlete athlete = new Athlete();
            Console.WriteLine("Insert athlete id: ");
            bool existingId = false;
            do
            {
                athlete.Id = int.Parse(Console.ReadLine());
                if (daoAthletes.FindRecord(athlete.Id) != null)
                {

                    existingId = false;
                }
                else
                {
                    Console.WriteLine("Athlete not found, \nPlease insert an existing id");
                    existingId = true;
                }
            } while (existingId != false);
            Console.WriteLine("Insert athlete name: ");
            athlete.AthleteName = Console.ReadLine();
            Console.WriteLine("Insert athlete surname: ");
            athlete.Surname = Console.ReadLine();
            Console.WriteLine("Insert athlete date of birth: ");
            bool rightDate = false;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                {
                    athlete.DateOfBirth = dateOfBirth;
                    rightDate = true;
                }
                else
                {
                    Console.WriteLine("Invalid date format \nPlease insert a valid format like 1999-12-12");
                    rightDate = false;
                }
            } while (rightDate != true);
            Console.WriteLine("Insert athlete country: ");
            athlete.Country = Console.ReadLine();
            if (daoAthletes.UpdateRecord(athlete))
            {
                Console.WriteLine("Athlete updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update athlete");
            }
        }
        public void FoundAthlete()
        {
            Console.WriteLine("Insert athlete id: ");
            int id = int.Parse(Console.ReadLine());
            Athlete athlete = (Athlete)daoAthletes.FindRecord(id);
            do
            {
                if (daoAthletes.FindRecord(id) == null)
                {
                    Console.WriteLine("Athlete not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoAthletes.FindRecord(id) == null);

            athlete = (Athlete)daoAthletes.FindRecord(id);
            if (athlete != null)
            {
                Console.WriteLine(athlete.ToString());
            }
            else
            {
                Console.WriteLine("Athlete not found");
            }
        }
        public void DeleteAthlete()
        {
            Console.WriteLine("Insert athlete id: ");
            int id = int.Parse(Console.ReadLine());
            do
            {
                if (daoAthletes.FindRecord(id) == null)
                {
                    Console.WriteLine("Athlete not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoAthletes.FindRecord(id) == null);

            if (daoAthletes.DeleteRecord(id))
            {
                Console.WriteLine("Athlete deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete athlete");
            }
        }
        public void ShowAllAthlete()
        {
            if (daoAthletes.GetRecords() != null)
            {
                foreach (Athlete athlete in daoAthletes.GetRecords())
                {
                    Console.WriteLine(athlete);
                }
            }
            else
            {
                Console.WriteLine("No athletes found");
            }
        }
        public void InsertEvent()
        {
            Event evento = new Event();
            bool newId = false;
            Console.WriteLine("Insert event id: ");
            do
            {
                evento.Id = int.Parse(Console.ReadLine());
                if (daoEvents.FindRecord(evento.Id) != null)
                {
                    Console.WriteLine("Event already exists, \nPlease insert a new id");
                    newId = true;
                }
                else
                {
                    newId = false;
                }
            } while (newId != false);
            Console.WriteLine("Insert event name: ");
            evento.EventName = Console.ReadLine();
            Console.WriteLine("Insert event year: ");
            bool rightYear = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int year))
                {
                    evento.EventYear = year;
                    rightYear = true;
                }
                else
                {
                    Console.WriteLine("Invalid year format \nPlease insert again the year");
                    rightYear = false;
                }
            } while (rightYear != true);
            Console.WriteLine("Insert event location: ");
            evento.EventLocation = Console.ReadLine();
            if (daoEvents.CreateRecord(evento))
            {
                Console.WriteLine("Event inserted successfully");
            }
            else
            {
                Console.WriteLine("Failed to insert event");
            }
        }
        public void UpdateEvent()
        {
            Event evento = new Event();
            Console.WriteLine("Insert event id: ");
            bool existingId = false;
            do
            {
                evento.Id = int.Parse(Console.ReadLine());
                if (daoEvents.FindRecord(evento.Id) != null)
                {
                    existingId = false;
                }
                else
                {
                    Console.WriteLine("Event not found, \nPlease insert an existing id");
                    existingId = true;
                }
            } while (existingId != false);
            Console.WriteLine("Insert event name: ");
            evento.EventName = Console.ReadLine();
            Console.WriteLine("Insert event year: ");
            bool rightYear = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out int year))
                {
                    evento.EventYear = year;
                    rightYear = true;
                }
                else
                {
                    Console.WriteLine("Invalid year format \nPlease insert the year again");
                    rightYear = false;
                }
            } while (rightYear != true);
            Console.WriteLine("Insert event location: ");
            evento.EventLocation = Console.ReadLine();
            if (daoEvents.UpdateRecord(evento))
            {
                Console.WriteLine("Event updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update event");
            }
        }
        public void DeleteEvent()
        {
            Console.WriteLine("Insert event id: ");
            int id = int.Parse(Console.ReadLine());
            do
            {
                if (daoEvents.FindRecord(id) == null)
                {
                    Console.WriteLine("Event not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }

            } while (daoEvents.FindRecord(id) == null);

            if (daoEvents.DeleteRecord(id))
            {
                Console.WriteLine("Event deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete event");
            }
        }
        public void FoundEvent()
        {
            Console.WriteLine("Insert event id: ");
            int id = int.Parse(Console.ReadLine());
            Event evento = (Event)daoEvents.FindRecord(id);
            do
            {
                if (daoEvents.FindRecord(id) == null)
                {
                    Console.WriteLine("Event not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoEvents.FindRecord(id) == null);

            evento = (Event)daoEvents.FindRecord(id);
            if (evento != null)
            {
                Console.WriteLine(evento.ToString());
            }
            else
            {
                Console.WriteLine("Event not found");
            }
        }
        public void ShowAllEvent()
        {
            if (daoEvents.GetRecords() != null)
            {
                foreach (Event evento in daoEvents.GetRecords())
                {
                    Console.WriteLine(evento);
                }
            }
            else
            {
                Console.WriteLine("No events found");
            }
        }
        public void InsertCompetition()
        {
            Competition competition = new Competition();
            bool newId = false;
            Console.WriteLine("Insert competition id: ");
            do
            {
                competition.Id = int.Parse(Console.ReadLine());
                if (daoCompetitions.FindRecord(competition.Id) != null)
                {
                    Console.WriteLine("Competition already exists, \nPlease insert a new id");
                    newId = true;
                }
                else
                {
                    newId = false;
                }
            } while (newId != false);
            Console.WriteLine("Insert competition name: ");
            competition.CompetitionName = Console.ReadLine();
            Console.WriteLine("Insert competition category: ");
            competition.Category = Console.ReadLine();
            Console.WriteLine("Is indoor competition? (y/n): ");
            string isIndoor = "";
            do
            {
                isIndoor = Console.ReadLine().ToLower();
                if (isIndoor == "y")
                {
                    competition.IsIndoor = true;
                }
                else if (isIndoor == "n")
                {
                    competition.IsIndoor = false;
                }
                else
                {
                    Console.WriteLine("Invalid option \nPlease insert again \"y\" or \"n\" ");
                }
            } while (isIndoor != "y" && isIndoor != "n");
            Console.WriteLine("Is team competition? (y/n): ");
            string isTeamCompetition = "";
            do
            {
                isTeamCompetition = Console.ReadLine().ToLower();
                if (isTeamCompetition == "y")
                {
                    competition.IsTeamCompetition = true;
                }
                else if (isTeamCompetition == "n")
                {
                    competition.IsTeamCompetition = false;
                }
                else
                {
                    Console.WriteLine("Invalid option \nPlease insert again \"y\" or \"n\" ");
                }
            } while (isTeamCompetition != "y" && isTeamCompetition != "n");
            if (daoCompetitions.CreateRecord(competition))
            {
                Console.WriteLine("Competition inserted successfully");
            }
            else
            {
                Console.WriteLine("Failed to insert competition");
            }

        }
        public void UpdateCompetition()
        {
            Competition competition = new Competition();
            Console.WriteLine("Insert competition id: ");
            bool existingId = false;
            do
            {
                competition.Id = int.Parse(Console.ReadLine());
                if (daoCompetitions.FindRecord(competition.Id) != null)
                {
                    existingId = false;
                }
                else
                {
                    Console.WriteLine("Competition not found, \nPlease insert an existing id");
                    existingId = true;
                }
            } while (existingId != false);
            Console.WriteLine("Insert competition name: ");
            competition.CompetitionName = Console.ReadLine();
            Console.WriteLine("Insert competition category: ");
            competition.Category = Console.ReadLine();
            Console.WriteLine("Is indoor competition? (y/n): ");
            string isIndoor = "";
            do
            {
                isIndoor = Console.ReadLine().ToLower();
                if (isIndoor == "y")
                {
                    competition.IsIndoor = true;
                }
                else if (isIndoor == "n")
                {
                    competition.IsIndoor = false;
                }
                else
                {
                    Console.WriteLine("Invalid option \nPlease insert \"y\" or \"n\"");
                }
            } while (isIndoor != "y" && isIndoor != "n");
            Console.WriteLine("Is team competition? (y/n): ");
            string isTeamCompetition = "";
            do
            {
                isTeamCompetition = Console.ReadLine().ToLower();
                if (isTeamCompetition == "y")
                {
                    competition.IsTeamCompetition = true;
                }
                else if (isTeamCompetition == "n")
                {
                    competition.IsTeamCompetition = false;
                }
                else
                {
                    Console.WriteLine("Invalid option \nPlease insert \"y\" or \"n\"");
                }
            } while (isTeamCompetition != "y" && isTeamCompetition != "n");
            if (daoCompetitions.UpdateRecord(competition))
            {
                Console.WriteLine("Competition updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update competition");
            }
        }
        public void DeleteCompetition()
        {
            Console.WriteLine("Insert competition id: ");
            int id = int.Parse(Console.ReadLine());
            do
            {
                if (daoCompetitions.FindRecord(id) == null)
                {
                    Console.WriteLine("Competition not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoCompetitions.FindRecord(id) == null);
            if (daoCompetitions.DeleteRecord(id))
            {
                Console.WriteLine("Competition deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete competition");
            }
        }
        public void FoundCompetition()
        {
            Console.WriteLine("Insert competition id: ");
            int id = int.Parse(Console.ReadLine());
            Competition competition = (Competition)daoCompetitions.FindRecord(id);
            do
            {
                if (daoCompetitions.FindRecord(id) == null)
                {
                    Console.WriteLine("Competition not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoCompetitions.FindRecord(id) == null);
            competition = (Competition)daoCompetitions.FindRecord(id);
            if (competition != null)
            {
                Console.WriteLine(competition.ToString());
            }
            else
            {
                Console.WriteLine("Competition not found");
            }
        }
        public void ShowAllCompetition()
        {
            if (daoCompetitions.GetRecords() != null)
            {
                foreach (Competition competition in daoCompetitions.GetRecords())
                {
                    Console.WriteLine(competition);
                }
            }
            else
            {
                Console.WriteLine("No competitions found");
            }
        }
        public void InsertMedal()
        {
            Medal medal = new Medal
            {
                Athlete = new Athlete(),
                Competition = new Competition(),
                Event = new Event()
            };
            bool newId = false;
            Console.WriteLine("Insert medal id: ");
            do
            {
                medal.Id = int.Parse(Console.ReadLine());
                if (daoMedals.FindRecord(medal.Id) != null)
                {
                    Console.WriteLine("Medal already exists, \nPlease insert a new id");
                    newId = true;
                }
                else
                {
                    newId = false;
                }
            } while (newId != false);
            Console.WriteLine("Insert medal tier 'bronze','silver' or 'gold': ");
            do
            {

                switch (Console.ReadLine().ToLower())
                {
                    case "bronze":
                        medal.MedalTier = "Bronzo";
                        break;
                    case "silver":
                        medal.MedalTier = "Argento";
                        break;
                    case "gold":
                        medal.MedalTier = "Oro";
                        break;
                    default:
                        Console.WriteLine("Invalid medal tier \nPlease insert a valid medal tier");
                        break;
                }
            } while (medal.MedalTier != "Bronzo" && medal.MedalTier != "Argento" && medal.MedalTier != "Oro");
            Console.WriteLine("Insert athlete id: ");
            do
            {
                medal.Athlete.Id = int.Parse(Console.ReadLine());
                if (daoAthletes.FindRecord(medal.Athlete.Id) == null)
                {
                    Console.WriteLine("Athlete not found, \nPlease insert an existing id");
                }
            } while (daoAthletes.FindRecord(medal.Athlete.Id) == null);

            Console.WriteLine("Insert competition id: ");
            do
            {
                medal.Competition.Id = int.Parse(Console.ReadLine());
                if (daoCompetitions.FindRecord(medal.Competition.Id) == null)
                {
                    Console.WriteLine("Competition not found, \nPlease insert an existing id");
                }
            } while (daoCompetitions.FindRecord(medal.Competition.Id) == null);
            Console.WriteLine("Insert event id: ");
            do
            {
                medal.Event.Id = int.Parse(Console.ReadLine());
                if (daoEvents.FindRecord(medal.Event.Id) == null)
                {
                    Console.WriteLine("Event not found, \nPlease insert an existing id");
                }
            } while (daoEvents.FindRecord(medal.Event.Id) == null);

            if (daoMedals.CreateRecord(medal))
            {
                Console.WriteLine("Medal inserted successfully");
            }
            else
            {
                Console.WriteLine("Failed to insert medal");
            }
        }
        public void UpdateMedal()
        {
            Medal medal = new Medal
            {
                Athlete = new Athlete(),
                Competition = new Competition(),
                Event = new Event()
            };

            Console.WriteLine("Insert medal id: ");
            bool existingId = false;
            do
            {
                medal.Id = int.Parse(Console.ReadLine());
                if (daoMedals.FindRecord(medal.Id) != null)
                {
                    existingId = false;
                }
                else
                {
                    Console.WriteLine("Medal not found, \nPlease insert an existing id");
                    existingId = true;
                }
            } while (existingId != false);
            Console.WriteLine("Insert medal tier 'bronze','silver' or 'gold': ");
            do
            {
                switch (Console.ReadLine().ToLower())
                {
                    case "bronze":
                        medal.MedalTier = "Bronzo";
                        break;
                    case "silver":
                        medal.MedalTier = "Argento";
                        break;
                    case "gold":
                        medal.MedalTier = "Oro";
                        break;
                    default:
                        Console.WriteLine("Invalid medal tier \nPlease insert a valid medal tier");
                        break;
                }
            } while (medal.MedalTier != "Bronzo" && medal.MedalTier != "Argento" && medal.MedalTier != "Oro");
            Console.WriteLine("Insert athlete id: ");
            do
            {
                medal.Athlete.Id = int.Parse(Console.ReadLine());
                if (daoAthletes.FindRecord(medal.Athlete.Id) == null)
                {
                    Console.WriteLine("Athlete not found, \nPlease insert an existing id");
                }
            } while (daoAthletes.FindRecord(medal.Athlete.Id) == null);
            Console.WriteLine("Insert competition id: ");
            do
            {
                medal.Competition.Id = int.Parse(Console.ReadLine());
                if (daoCompetitions.FindRecord(medal.Competition.Id) == null)
                {
                    Console.WriteLine("Competition not found, \nPlease insert an existing id");
                }
            } while (daoCompetitions.FindRecord(medal.Competition.Id) == null);
            Console.WriteLine("Insert event id: ");
            do
            {
                medal.Event.Id = int.Parse(Console.ReadLine());
                if (daoEvents.FindRecord(medal.Event.Id) == null)
                {
                    Console.WriteLine("Event not found, \nPlease insert an existing id");
                }
            } while (daoEvents.FindRecord(medal.Event.Id) == null);
            Console.WriteLine(daoMedals.UpdateRecord(medal) ? "Medal updated successfully" : "Failed to update medal");
        }
        public void DeleteMedal()
        {
            Console.WriteLine("Insert medal id: ");
            int id = int.Parse(Console.ReadLine());
            do
            {
                if (daoMedals.FindRecord(id) == null)
                {
                    Console.WriteLine("Medal not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoMedals.FindRecord(id) == null);
            if (daoMedals.DeleteRecord(id))
            {
                Console.WriteLine("Medal deleted successfully");
            }
            else
            {
                Console.WriteLine("Failed to delete medal");
            }
        }
        public void FoundMedal()
        {
            Console.WriteLine("Insert medal id: ");
            int id = int.Parse(Console.ReadLine());
            Medal medal = (Medal)daoMedals.FindRecord(id);
            do
            {
                if (daoMedals.FindRecord(id) == null)
                {
                    Console.WriteLine("Medal not found, \nPlease insert an existing id");
                    id = int.Parse(Console.ReadLine());
                }
            } while (daoMedals.FindRecord(id) == null);
            medal = (Medal)daoMedals.FindRecord(id);
            if (medal != null)
            {
                Console.WriteLine(medal.ToString());
            }
            else
            {
                Console.WriteLine("Medal not found");
            }
        }       
        public void ShowAllMedals()
        {
            if (daoMedals.GetRecords() != null)
            {
                foreach (Medal medal in daoMedals.GetRecords())
                {
                    Console.WriteLine(medal.ToString());
                }
            }
            else
            {
                Console.WriteLine("No medals found");
            }
        }

    #endregion
    }

}
