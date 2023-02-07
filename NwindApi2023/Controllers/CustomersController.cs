
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NwindApi2023.Models;

namespace NwindApi2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Tietokantakontekstin alustus
        private readonly NorthwindContext db = new NorthwindContext();


        // Hakee kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                List<Customer> customers = db.Customers.ToList();
                return Ok(customers);
            }

            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe: " + e.Message);
            }

        }


        // Hakee asiakkaan id:n perusteella
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(string id)
        {
            try
            {
                Customer cust = db.Customers.Find(id);

                if (cust != null)
                {
                    return Ok(cust);
                }
                else
                {
                    return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
                }
            }

            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe: " + e.Message);
            }

        }


        // Hakee asiakkaan Company namen perusteella
        [HttpGet]
        [Route("cname/{cname}/country/{country}")]
        public ActionResult GetByCompanyName(string cname, string country)
        {
            try
            {
                //var cust = (from c in db.Customers where c.CompanyName == cname select c).ToList();
                //var cust = db.Customers.Where(c => c.CompanyName == cname);

                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname) &&
                country == c.Country).ToList();

                if (cust.Count > 0)
                {
                    return Ok(cust);
                }
                else
                {
                    return NotFound("Asiakasta hakusanalla " + cname + " ja " + country + " ei löytynyt.");
                }
            }

            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe: " + e.Message);
            }

        }

    }
}
