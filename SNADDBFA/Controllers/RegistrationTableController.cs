using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNADDBFA.Models;

namespace SNADDBFA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationTableController : ControllerBase
    {
        private readonly SnaddbfaContext _context;

        public RegistrationTableController(SnaddbfaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getall")]


        public async Task<ActionResult<IEnumerable<RegistrationTable>>> Getall()
        {
            if (_context.RegistrationTables == null)
            {
                return NotFound();
            }
            return await _context.RegistrationTables.ToListAsync();
        }

        
        [HttpGet]
        [Route("getbyfilters")]


        public async Task<List<RegistrationTable>> GetAllWithFilters(string? userId, string? name, string? country, string? email, bool sex, bool language)
        {
            var query = _context.RegistrationTables.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.UserId == userId);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(x => x.Country == country);
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email == email);
            }

            if (!sex)
            {
                query = query.Where(x => x.Sex == sex);
            }

            if (!language)
            {
                query = query.Where(x => x.Language == language);
            }

            var result = await query.ToListAsync();
            return result;
        }


        [HttpGet]
        [Route("getbyId")]


        public async Task<ActionResult<RegistrationTable>> GetbyId(int sno)
        {
            

            return await _context.RegistrationTables.Where(x => x.Sno == sno).FirstAsync();

        }


        [HttpPost]
        [Route("/hfhfhf/Post")]

        //public async Task<ActionResult<RegistrationTable>> Post(RegistrationTable asset)
        //{
        //    var res = await _context.AddAsync(asset);
        //    await _context.SaveChangesAsync();
        //    return res.Entity;

        //}


        public async Task<ActionResult<RegistrationTable>> PostForm([FromBody] RegistrationTable type)
        {
            // Validate UserId
            if (string.IsNullOrEmpty(type.UserId) || type.UserId.Length < 5 || type.UserId.Length > 12)
            {
                ModelState.AddModelError("UserId", "UserId is required and must be between 5 to 12 characters.");
            }

            // Validate Password
            if (string.IsNullOrEmpty(type.Password) || type.Password.Length < 7 || type.Password.Length > 12)
            {
                ModelState.AddModelError("Password", "Password is required and must be between 7 to 12 characters.");
            }

            // Validate Name (Alphabets only)
            if (string.IsNullOrEmpty(type.Name) || !type.Name.All(char.IsLetter))
            {
                ModelState.AddModelError("Name", "Name is required and must contain only alphabets.");
            }

            // Validate Email (Valid Email)
            if (string.IsNullOrEmpty(type.Email) || !IsValidEmail(type.Email))
            {
                ModelState.AddModelError("Email", "Email is required and must be a valid email address.");
            }

            // Check if UserId and Email are unique
            if (await _context.RegistrationTables.AnyAsync(x => x.UserId.ToLower() == type.UserId.ToLower()))
            {
                ModelState.AddModelError("UserId", "UserId must be unique.");
            }

            if (await _context.RegistrationTables.AnyAsync(x => x.Email == type.Email))
            {
                ModelState.AddModelError("Email", "Email must be unique.");
            }

            // Return 400 Bad Request if there are validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //posting
            var res = await _context.AddAsync(type);
              await _context.SaveChangesAsync();
                return res.Entity;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]


        public async Task<RegistrationTable> Update(RegistrationTable type)
        {
            var update = await _context.RegistrationTables
                .Where(x => x.Sno.Equals(type.Sno))
                .FirstOrDefaultAsync();

            if (update != null)
            {
                
                if (type.UserId != null)
                    update.UserId = type.UserId;

                if (type.Password != null)
                    update.Password = type.Password;

                if (type.Name != null)
                    update.Name = type.Name;

                if (type.Address != null)
                    update.Address = type.Address;

                if (type.Country != null)
                    update.Country = type.Country;

                if (type.Zipcode != null)
                    update.Zipcode = type.Zipcode;

                if (type.Email != null)
                    update.Email = type.Email;

                if (type.Sex != null)
                    update.Sex = type.Sex;

                if (type.About != null)
                    update.About = type.About;

                await _context.SaveChangesAsync();
            }

            return update;
        }

        //public async Task<RegistrationTable> Update(RegistrationTable type)
        //{
        //    var update = await _context.RegistrationTables.Where(x => x.Sno.Equals(type.Sno)).FirstOrDefaultAsync();

        //    if (update != null)

        //    {
        //        update.UserId = type.UserId;
        //        update.Password = type.Password;
        //        update.Name = type.Name;
        //        update.Address = type.Address;
        //        update.Country = type.Country;
        //        update.Zipcode = type.Zipcode;
        //        update.Email = type.Email;
        //        update.Sex = type.Sex;
        //        update.About = type.About;
        //        update.Sno = type.Sno;

        //        await _context.SaveChangesAsync();
        //    }

        //    return update;
        //}

        [HttpDelete]

        public async Task<Boolean> Delete(int sno)
        {
            var deleteData = await _context.RegistrationTables
        .Where(x => x.Sno.Equals(sno))
        .FirstOrDefaultAsync();

            if (deleteData != null)
            {
                _context.RegistrationTables.Remove(deleteData);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }








    }
}
