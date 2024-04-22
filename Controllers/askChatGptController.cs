using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiChatGPT.Interfaces;

namespace webApiChatGPT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class askChatGptController : ControllerBase
    {
        public readonly IChatGptService _chatGptService;

        public askChatGptController(IChatGptService chatGptService)
        {
            _chatGptService = chatGptService;
        }
        [HttpPost("getresponse")]
        public async Task<IActionResult> GetResponse([FromBody] string prompt)
        {

            if (string.IsNullOrWhiteSpace(prompt))
            {

                return BadRequest("Promt is Required");

            }

            try { 
            var response = await _chatGptService.GetResponseAsync(prompt);
                return Ok(response);
                            
            }
            catch (SystemException ex) {
            
                return StatusCode(500, ex.Message); 
            
            }

        }
    }
}
