﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lottery.Entities;
using Lottery.Models;
using Lottery.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Controllers
{
    [ApiController]
    [Route("api/rounds/{roundId}/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoundRepository _roundRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(
            IMapper mapper,
            IRoundRepository roundRepository,
            IStudentRepository studentRepository)
        {
            _mapper = mapper;
            _roundRepository = roundRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet(Name = nameof(GetAllStudentForRound))]
        public async Task<IActionResult> GetAllStudentForRound(string roundId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entities = await _studentRepository.GetAllStudentsForRoundAsync(roundId);

            var models = _mapper.Map<IEnumerable<StudentViewModel>>(entities);

            return Ok(models);
        }

        [HttpGet("{studentId}", Name = nameof(GetStaffForRound))]
        public async Task<IActionResult> GetStaffForRound(string roundId, string studentId)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = await _studentRepository.GetStudentForRound(roundId, studentId);
            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<StudentViewModel>(entity);

            return Ok(model);
        }

        [HttpPost("{roundId}", Name = nameof(CreateStaff))]
        public async Task<IActionResult> CreateStaff(string roundId, StaffAddViewModel model)
        {
            if (!await _roundRepository.RoundExistsAsync(roundId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Student>(model);

            _studentRepository.AddStudentForRound(roundId, entity);
            var result = await _studentRepository.SaveAsync();

            if (result)
            {
                var returnModel = _mapper.Map<StaffViewModel>(entity);
                return CreatedAtRoute(nameof(GetStaffForRound), new { roundId, studentId = returnModel.Id }, returnModel);
            }

            return BadRequest();
        }
    }
}