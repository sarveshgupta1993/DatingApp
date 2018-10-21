﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    public class ValuesController : ControllerBase
    {
      private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context=context;
        }

        [AllowAnonymous]
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var values= Task.Run(()=> _context.Values.ToList());
            var result=await values;
            return Ok(result);
            
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var value =  Task.Run(()=> _context.Values.FirstOrDefault(x=>x.Id==id));
            var result1= await value;
            return Ok(result1);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
