﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private ISubmissionService service;
        private IMapper mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET api/submission
        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            if (!challengeId.HasValue || !accelerationId.HasValue)
                return NoContent();

            return Ok(this.service.FindByChallengeIdAndAccelerationId(challengeId.Value, accelerationId.Value).
                    Select(x => mapper.Map<SubmissionDTO>(x)).
                    ToList());
        }

        // GET api/submission/higherScore
        [HttpGet("higherScore")]
        public ActionResult<decimal> GetHigherScore(int? challengeId = null)
        {
            if (!challengeId.HasValue)
                return NoContent();

            return Ok(service.FindHigherScoreByChallengeId(challengeId.Value));
        }

        // POST api/submission
        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(mapper.Map<SubmissionDTO>(service.Save(mapper.Map<Submission>(value))));
        }
    }
}
