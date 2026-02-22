using MovieScreeningsManager.Common.Enums;
using MovieScreeningsManager.DBModels;
using MovieScreeningsManager.Services;

namespace MovieScreeningsManager.UIModels
{
    public class CinemaHallUIModel
    {
        private CinemaHallDBModel _dbModel;
        private Guid? _id;
        private string _name;
        private int _capacity;
        private CinemaHallType _type;
        private TimeSpan? _totalScreeningsTime;
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
        public TimeSpan TotalScreeningsTime 
        {
            get
            {
                calculateTotalScreeningsTime();
                return _totalScreeningsTime ?? TimeSpan.Zero;
            }
            private set => _totalScreeningsTime = value; 
        }


        public CinemaHallUIModel()
        {
            _screenings = new List<ScreeningUIModel>();
        }

        // A constructor for creating a CinemaHallUIModel based on an existing CinemaHallDBModel
        public CinemaHallUIModel(CinemaHallDBModel dbModel) : this()
        {
            _dbModel = dbModel;
            _id = dbModel.Id;
            _name = dbModel.Name;
            _capacity = dbModel.Capacity;
            _type = dbModel.Type;
            calculateTotalScreeningsTime();
        }


        /// <summary>
        /// Calculates the total duration of all screenings in the schedule.
        /// </summary>
        private void calculateTotalScreeningsTime()
        {
            TimeSpan totalTime = TimeSpan.Zero;
            foreach (var screening in _screenings)
            {
                totalTime = totalTime.Add(TimeSpan.FromMinutes(screening.Duration));
            }
            _totalScreeningsTime = totalTime;
        }

        /// <summary>
        /// saves the changes made to the UI model back to the underlying DB model.
        /// if the db model does not exist, 
        /// it creates a new one and assigns it UI model attributes
        /// </summary>
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

        /// <summary>
        /// Loads the screenings associated with this cinema hall 
        /// from the storage service and populates the Screenings collection.
        /// </summary>
        /// <param name="screeningDBModels"></param>
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
