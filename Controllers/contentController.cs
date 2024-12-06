using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("Testtablecontent")]
    [ApiController]
    public class contentController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Testtablecontent> PostContent(PostContent postContent)
        {
            using (var context = new Test2Context())
            {
                Testtablecontent newContent = new Testtablecontent()
                {
                    ContentId = Guid.NewGuid(),
                    Content=postContent.content,
                    Userid = postContent.userId,

                    
                };
                if (newContent !=null)
                {
                    context.Testtablecontents.Add(newContent);
                    context.SaveChanges();
                    return StatusCode(201, newContent);
                }
                return BadRequest();
            }
        }
    }
}
