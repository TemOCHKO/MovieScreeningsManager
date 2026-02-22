using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.Services;

namespace MovieScreeningsManager.UIModels
{
    public class CinemaHallUIModel
    {

        // 
        private CinemaHallDBModel _dbModel;
        private Guid? _id;
        private string _name;
        private int _capacity;
        private CinemaHallType _type;
        private List<ScreeningUIModel> _screenings;


        // Properties to expose the fields of the UI model.
        // The ID is read-only,
        // and it should not be changed after creation.

        public Guid? Id { get => _dbModel.Id; }
        public string Name 
        {
            get => _name;
            set => _name = value;
        }
        public int Capacity 
        {
            get => _capacity; 
            set => _capacity = value; 
        }
        public CinemaHallType Type 
        { 
            get => _type; 
            set => _type = value; 
        }
        public IReadOnlyList<ScreeningUIModel> Screenings 
        {
            get => _screenings; 
        }

        /// <summary>
        /// Gets the duration of all screenings in the cinema hall.
        /// </summary>
        /// <remarks>This property is read-only and is automatically updated to reflect the total time of
        /// all screenings..</remarks>
        public TimeSpan TotalScreeningsTime { get => calculateTotalScreeningsTime(); private set; }


        // A constructor for creating a new cinema hall without an existing DB model
        public CinemaHallUIModel()
        {
            _screenings = new List<ScreeningUIModel>();
            TotalScreeningsTime = calculateTotalScreeningsTime();
        }

        // A constructor for creating a CinemaHallUIModel based on an existing CinemaHallDBModel
        public CinemaHallUIModel(CinemaHallDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _capacity = dbModel.Capacity;
            _type = dbModel.Type;
        }


        /// <summary>
        /// Calculates the total duration of all screenings in the schedule.
        /// </summary>
        private TimeSpan calculateTotalScreeningsTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;
            foreach (var screening in _screenings)
            {
                totalTime = totalTime.Add(TimeSpan.FromMinutes(screening.Duration));
            }
            return totalTime;
        }

        public void SaveChangesToDBModel()
        {
            if (_dbModel == null)
            {
                _dbModel = new CinemaHallDBModel(_name, _capacity, _type);
                _id = _dbModel.Id;
            }
            else
            {
                _dbModel.Name = _name;
                _dbModel.Capacity = _capacity;
                _dbModel.Type = _type;
            }
        }

        public void LoadScreenings(StorageService screeningDBModels)
        {
            if (Id == null || _screenings.Count > 0) return;

            foreach (var screeningDBModel in screeningDBModels.GetScreenings(Id.Value))
            {
                _screenings.Add(new ScreeningUIModel(screeningDBModel));
            }
        }

        public override string ToString()
        {
            return $"Cinema Hall: {Name}, Capacity: {Capacity}, Type: {Type}, Total Screenings Time: {TotalScreeningsTime}";
        }
    }
}
