using MovieScreeningsManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.DTOModels.CinemaHalls
{
    public class CinemaHallCreateDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public CinemaHallType Type { get; set; }
        public int RowCount { get; set; }

        public CinemaHallCreateDTO(string name, int capacity, CinemaHallType type, int rowCount) : this(Guid.NewGuid(), name, capacity, type, rowCount)
        {

        }
        public CinemaHallCreateDTO(Guid id, string name, int capacity, CinemaHallType type, int rowCount)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Type = type;
            RowCount = rowCount;
        }

    }
}
