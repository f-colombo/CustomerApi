using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Models.SQLite;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerSQLiteRepository _sqliteRepository;

        public CustomersController(CustomerSQLiteRepository sqliteRepository)
        {
            _sqliteRepository = sqliteRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _sqliteRepository.GetAll();

            if (customers == null)
            {
                return NotFound();
            }

            return new ObjectResult(customers);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetById(long id)
        {
            var customer = _sqliteRepository.GetById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return new ObjectResult(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerRecord customer)
        {
            CustomerRecord created = _sqliteRepository.Create(customer);

            return CreatedAtRoute("GetCustomer", new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] CustomerRecord customer)
        {
            var record = _sqliteRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            customer.Id = id;
            _sqliteRepository.Update(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var record = _sqliteRepository.GetById(id);

            if (record == null)
            {
                return NotFound();
            }

            _sqliteRepository.Remove(id);

            return NoContent();
        }

        #region "Original Code"

        //// GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #endregion
    }
}
