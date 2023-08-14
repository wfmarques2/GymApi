using AutoMapper;
using GymApi.Data;
using GymApi.Dtos.Request;
using GymApi.Dtos.Response;
using GymApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GymApi.Controllers
{
    [ApiController]
    [Route("alunos/{alunoId}/treinos")]
    public class WorkoutController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IMapper _mapper;

        public WorkoutController(GymContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllWorkouts(int alunoId)
        {
            var workouts = _context.Workouts
                .Include(w => w.ExerciseDetails)
                .Where(w => w.ClientId == alunoId)
                .ToList();

            var dto = _mapper.Map<ICollection<ReadWorkoutDto>>(workouts);

            return Ok(dto);
        }

        [HttpGet("{treinoId}")]
        public IActionResult GetWorkoutById(int alunoId, int treinoId)
        {
            var workout = _context.Workouts
                .Include(w => w.ExerciseDetails)
                .SingleOrDefault(w => w.Id == treinoId && w.ClientId == alunoId);

            if (workout is null) return NotFound();

            var dto = _mapper.Map<ReadWorkoutDto>(workout);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult AddWorkout(int alunoId, [FromBody] CreateWorkoutDto dto)
        {
            var workout = _mapper.Map<Workout>(dto);
            workout.ClientId = alunoId;

            _context.Workouts.Add(workout);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetWorkoutById), new { alunoId, treinoId = workout.Id }, workout);
        }

        [HttpPut("{treinoId}")]
        public IActionResult UpdateWorkout(int alunoId, int treinoId, [FromBody] UpdateWorkoutDto dto)
        {
            var workout = _context.Workouts
                .Include(w => w.ExerciseDetails)
                .SingleOrDefault(w => w.Id == treinoId && w.ClientId == alunoId);

            if (workout is null) return NotFound();

            _mapper.Map(dto, workout);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{treinoId}")]
        public IActionResult DeleteWorkout(int alunoId, int treinoId)
        {
            var workout = _context.Workouts
                .SingleOrDefault(w => w.Id == treinoId && w.ClientId == alunoId);

            if (workout is null) return NotFound();

            _context.Workouts.Remove(workout);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
