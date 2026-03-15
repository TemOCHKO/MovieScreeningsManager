using System.ComponentModel.DataAnnotations;

namespace MovieScreeningsManager.Common.Enums
{
    public enum CinemaHallType
    {
        [Display(Name = "Standard")]
        Standard,
        [Display(Name = "IMAX")]
        IMAX,
        [Display(Name = "3D")]
        ThreeD,
        [Display(Name = "4DX")]
        FourDX,
    }
}
