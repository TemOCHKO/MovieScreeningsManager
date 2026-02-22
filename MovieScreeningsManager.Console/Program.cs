using MovieScreeningsManager.Services;
using MovieScreeningsManager.UIModels;

namespace MovieScreeningsManagerConsole
{
    internal class Program
    {
        enum AppState
        {
            Default = 0,
            CinemaHallDetails = 1,
            End = 2,
            Exit = 100,
        }

        private static AppState _appState = AppState.Default;
        private static StorageService _storageService;
        private static List<CinemaHallUIModel> _cinemaHalls;

        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello and welcome to the Movie Screenings Manager Console App!");
            _storageService = new StorageService();
            string? command = null;
            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.CinemaHallDetails:
                        CinemaHallDetailsState(command);
                        break;
                    case AppState.Default:
                        DefaultState();
                        break;
                }
                System.Console.WriteLine("Type Exit to close application.");
                command = System.Console.ReadLine();
                UpdateState(command);
            }
        }

        private static void UpdateState(string? command)
        {
            command = command?.Trim();
            command = command?.ToLower();
            switch (command)
            {
                case "back":
                    _appState = AppState.Default;
                    break;
                case "exit":
                    _appState = AppState.Exit;
                    System.Console.WriteLine("Thank you and see you later!");
                    break;
                default:
                    switch (_appState)
                    {
                        case AppState.Default:
                            _appState = AppState.CinemaHallDetails;
                            break;
                        case AppState.End:
                            System.Console.WriteLine("Unknown command. Please try again.");
                            break;
                    }
                    break;
            }
        }

        private static void DefaultState()
        {
            System.Console.WriteLine("Here is the list of all CinemaHalls: ");
            LoadCinemaHalls();
            foreach (var cinemaHall in _cinemaHalls)
            {

                System.Console.WriteLine(cinemaHall);
            }    
            System.Console.WriteLine("Type the name of CinemaHall to see it's Screenings.");
            
        }


        private static void LoadCinemaHalls()
        {
            if (_cinemaHalls != null)
                return;
            _cinemaHalls = new List<CinemaHallUIModel>();
            foreach (var cinemaHall in _storageService.GetAllCinemaHalls())
            {
                var cinemaHallUIModel = new CinemaHallUIModel(cinemaHall);
                cinemaHallUIModel.LoadScreenings(_storageService);
                _cinemaHalls.Add(cinemaHallUIModel);
            }
        }

        private static void CinemaHallDetailsState(string? cinemaHallName)
        {
            cinemaHallName = cinemaHallName?.Trim();
            cinemaHallName = cinemaHallName?.ToLower();
            bool cinemaHallExists = false;
            foreach (var cinemaHall in _cinemaHalls)
            {
                if (cinemaHall.Name.ToLower() == cinemaHallName)
                {
                    cinemaHallExists = true;
                    cinemaHall.LoadScreenings(_storageService);

                    if (cinemaHall.Screenings.Count <= 0)
                    {
                        System.Console.WriteLine($"No screenings found in {cinemaHall.Name}.");

                    }
                    else
                    {
                        System.Console.WriteLine($"Screenings in {cinemaHall.Name}:");
                        foreach (var screening in cinemaHall.Screenings)
                        {
                            System.Console.WriteLine(screening);
                            System.Console.WriteLine();
                        }
                    }
                    System.Console.WriteLine();
                }
            }
            if (!cinemaHallExists)
            {
                System.Console.WriteLine("Cinema Hall not found. Please try again.");
                System.Console.WriteLine();
            }
            else
            {
                System.Console.WriteLine("Type Back to get list of all Cinema Halls.");
                _appState = AppState.End;
            }
        }
    }

        
}
