using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("testtableuser")]
    [ApiController]
    public class userController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Testtableuser> PostUser(PostUserDTO postUserDTO)//A post metódus létrehozása
        {
			Testtableuser user = new Testtableuser()//létrehozunk egy user változót az adattagokkal együtt, melyek közül a nevet a dto-n keresztül kérjuk be.
			{
				Id = Guid.NewGuid(),//guid 36 karakter hosszú egyedi azonosító.
				Name = postUserDTO.name,//név, a dto-ból szedjük ki.
			};
			if (user != null)//ha van írva mind a két helyre, tehát helyes a bemeneti érték, akkor:
			{
				using (var context = new Test2Context())//használjuk a scaffolding után létrehozott context-et.
				{

					context.Testtableusers.Add(user);//hozzá adjuk az új user-t a testtableusers-hez,
					context.SaveChanges();//mentjük a változást
					return StatusCode(201, user);//majd visszatérünk egy 201-es státuszkóddal + az új elemmel.
				}
			}
			else
			{
				return BadRequest();//más esetben hibát hoz a végpont.
			}
        }
		[HttpGet] //get metódus
		public ActionResult<Testtableuser> GetUsers()//a get végpont létrehozása
		{
			using (var context = new Test2Context())//használjuk a scaffolding után létrehozott context-et.
			{
				return Ok(context.Testtableusers.Include(x=>x.Testtablecontents).ToList());//visszatérünk egy 200-as státuszkóddal + az elemekkel kilistázva.
			}
		}

		[HttpPut]
		public ActionResult<Testtableuser> UpdateUser(Guid id, PostUserDTO newData)
		{
			using (var context = new Test2Context())
			{
				Testtableuser existingUser =context.Testtableusers.FirstOrDefault(x=>x.Id==id);
				if (existingUser != null)
				{
					existingUser.Name = newData.name;
					context.Testtableusers.Update(existingUser);
					context.SaveChanges();
					return Ok(context.Testtableusers.ToList());

				}
				return BadRequest();
			}
		}
		[HttpDelete]
		public ActionResult<Testtableuser> DeleteUser(Guid id)
		{
			using (var context = new Test2Context())
			{
				Testtableuser deletingUser=context.Testtableusers.FirstOrDefault(y=>y.Id==id);
				if (deletingUser != null)
				{
					context.Testtableusers.Remove(deletingUser);
					context.SaveChanges();
					return Ok( new {message="Sikeres törlés!" });
				}
				return NotFound();
			}
		}
    }
}
