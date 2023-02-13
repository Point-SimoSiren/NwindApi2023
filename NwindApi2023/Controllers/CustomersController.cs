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
        [Route("cname/{cname}")]
        public ActionResult GetByCompanyName(string cname)
        {
            try
            {
                //var cust = (from c in db.Customers where c.CompanyName.Contains(cname) select c).ToList();

                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname)).ToList();

                if (cust.Count > 0)
                {
                    return Ok(cust);
                }
                else
                {
                    return NotFound($"Asiakasta hakusanalla {cname} ei löytynyt.");
                }
            }

            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe: " + e.Message);
            }
        }

        // Uuden asiakkaan luominen
        [HttpPost]
        public ActionResult CreateNew([FromBody] Customer newCustomer)
        {

            try
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                return Ok($"Asiakas {newCustomer.CompanyName} lisätty onnistuneesti");
            }
            catch (Exception e)
            {
                return BadRequest($"Tapahtui virhe tyypiltään: {e.GetType().Name}. " +
                    $"Lue lisää: {e.InnerException}");
            }
        }

        // Asiakkaan poistaminen
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            Customer cust = db.Customers.Find(id);
            if (cust != null)
            {
                try
                {
                    db.Customers.Remove(cust);
                    db.SaveChanges();
                    return Ok($"Asiakas {cust.CompanyName} poistettiin.");
                }

                catch (Exception e)
                {
                    return BadRequest(e.InnerException);
                }
            }
            else
            {
                return NotFound($"Asiakasta id:llä {id} ei löytynyt.");
            }
        }



    }
}
