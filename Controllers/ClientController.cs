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
    [Route("alunos")]
    public class ClientController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IMapper _mapper;

        public ClientController(IMapper mapper, GymContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var clients = _context.Clients.ToList();
            var dto = _mapper.Map<ICollection<ReadClientDto>>(clients);

            return Ok(dto);
        }

        [HttpGet("{alunoId}")]
        public IActionResult GetClientById(int alunoId)
        {
            var client = _context.Clients.Find(alunoId);
            if (client is null) return NotFound();

            var dto = _mapper.Map<ReadClientDto>(client);

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] CreateClientDto dto)
        {
            var client = _mapper.Map<Client>(dto);

            _context.Clients.Add(client);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClientById), new { alunoId = client.Id }, client);
        }

        [HttpPut("{alunoId}")]
        public IActionResult UpdateClient(int alunoId, [FromBody] UpdateClientDto dto)
        {
            var client = _context.Clients.Find(alunoId);
            if (client is null) return NotFound();

            _mapper.Map(dto, client);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{alunoId}")]
        public IActionResult DeleteClient(int alunoId)
        {
            var client = _context.Clients.Find(alunoId);
            if (client is null) return NotFound();

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
