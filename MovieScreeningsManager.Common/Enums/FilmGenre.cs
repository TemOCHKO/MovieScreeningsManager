using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieScreeningsManager.Common.Enums
{
    public enum FilmGenre
    {
        [Display(Name = "Comedy")]
        Comedy,
        [Display(Name = "Thriller")]
        Thriller,
        [Display(Name = "Tragedy")]
        Tragedy,
        [Display(Name = "Drama")]
        Drama,
        [Display(Name = "Fiction")]
        Fiction,
        [Display(Name = "Tragi-Comedy")]
        Tragicomedy,
        [Display(Name = "Action")]
        Action,
        [Display(Name = "Horror")]
        Horror,
        [Display(Name = "Science-Fiction")]
        ScienceFiction,
        [Display(Name = "Fantasy")]
        Fantasy,
        [Display(Name = "Mystery")]
        Mystery,
        [Display(Name = "Romance")]
        Romance,
        [Display(Name = "Animation")]
        Animation,
        [Display(Name = "Documentary")]
        Documentary,
        [Display(Name = "Crime")]
        Crime, 
    }
}
