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
    [Route("alunos/{alunoId}/treinos/{treinoId}/detalhesExercicios")]
    public class ExerciseDetailController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IMapper _mapper;

        public ExerciseDetailController(GymContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllExerciseDetails(int alunoId, int treinoId)
        {
            var exerciseDetails = _context.ExercisesDetail
                .Where(ed => ed.WorkoutId == treinoId && ed.Workout.ClientId == alunoId)
                .ToList();

            var dto = _mapper.Map<ICollection<ReadExerciseDetailDto>>(exerciseDetails);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult GetExerciseDetailById(int alunoId, int treinoId, int id)
        {
            var exerciseDetail = _context.ExercisesDetail
                .SingleOrDefault(ed => ed.Id == id && ed.WorkoutId == treinoId && ed.Workout.ClientId == alunoId);

            if (exerciseDetail is null) return NotFound();

            var dto = _mapper.Map<ReadExerciseDetailDto>(exerciseDetail);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult AddExerciseDetail(int alunoId, int treinoId, [FromBody] CreateExerciseDetail dto)
        {
            var exerciseDetail = _mapper.Map<ExerciseDetail>(dto);
            exerciseDetail.WorkoutId = treinoId;

            _context.ExercisesDetail.Add(exerciseDetail);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetExerciseDetailById), new { alunoId, treinoId, id = exerciseDetail.Id }, exerciseDetail);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExerciseDetail(int alunoId, int treinoId, int id, [FromBody] UpdateExerciseDetailDto dto)
        {
            var exerciseDetail = _context.ExercisesDetail
                .SingleOrDefault(ed => ed.Id == id && ed.WorkoutId == treinoId && ed.Workout.ClientId == alunoId);

            if (exerciseDetail is null) return NotFound();

            _mapper.Map(dto, exerciseDetail);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExerciseDetail(int alunoId, int treinoId, int id)
        {
            var exerciseDetail = _context.ExercisesDetail
                .SingleOrDefault(ed => ed.Id == id && ed.WorkoutId == treinoId && ed.Workout.ClientId == alunoId);

            if (exerciseDetail is null) return NotFound();

            _context.ExercisesDetail.Remove(exerciseDetail);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
