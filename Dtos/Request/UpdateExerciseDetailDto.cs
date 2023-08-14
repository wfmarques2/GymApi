using System.ComponentModel.DataAnnotations;

namespace GymApi.Dtos.Request;

public class UpdateExerciseDetailDto
{
    [Required]
    public int ExerciseId { get; set; }

    [Required]
    public int WorkoutId { get; set; }

    [Required]
    public double Load { get; set; }

    [Required]
    public int Rep { get; set; }

    [Required]
    public int Series { get; set; }

    [Required]
    [RegularExpression(@"^([01]?[0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$", ErrorMessage = "RestTime deve estar no formato 'HH:mm:ss'.")]
    public TimeSpan RestTime { get; set; }
}


