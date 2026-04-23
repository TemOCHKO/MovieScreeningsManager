using MovieScreeningsManager.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.DTOModels.CinemaHalls
{
    public class CinemaHallEditDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public CinemaHallType Type { get; set; }
        public int RowCount { get; set; }
        public CinemaHallEditDTO(Guid id, string name, int capacity, CinemaHallType type, int rowCount)
        {
            Id = id;
            Name = name;
            Capacity = capacity;
            Type = type;
            RowCount = rowCount;
        }
    }
}
