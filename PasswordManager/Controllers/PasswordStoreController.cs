using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Data;
using PasswordManager.Helper;
using PasswordManager.Models;
using PasswordManager.Models.Dtos;

namespace PasswordManager.Controllers
{
    [Route("api/passwordstore")]
    [ApiController]
    public class PasswordStoreController : ControllerBase
    {
        private readonly PasswordManagerDbContext _dbContext;

        public PasswordStoreController(PasswordManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PasswordStoreItem>> GetPasswordStoreItems()
        {
            var passwordStoreItems = _dbContext.PasswordStoreItems.Select(p => p.ToDto());

            return Ok(passwordStoreItems);
        }

        [HttpGet("{id:int}", Name = "GetPasswordStoreItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PasswordStoreItemDto> GetPasswordStoreItem([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var passwordStoreItem = _dbContext.PasswordStoreItems.Find(id);

            if (passwordStoreItem == null)
                return NotFound();

            var passwordStoreItemDto = passwordStoreItem.ToDto();

            return Ok(passwordStoreItemDto);
        }

        [HttpGet("decrypted/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PasswordStoreItemDto> GetDecryptedPasswordStoreItem([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var passwordStoreItem = _dbContext.PasswordStoreItems.Find(id);

            if (passwordStoreItem == null)
                return NotFound();

            var passwordStoreItemDto = passwordStoreItem.ToDto();
            passwordStoreItemDto.Password = PasswordEncryption.GetDecryptedPassword(passwordStoreItem.EncryptedPassword);

            return Ok(passwordStoreItemDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PasswordStoreItemDto> CreatePasswordStoreItem([FromBody] PasswordStoreItemDto passwordStoreItemDto)
        {
            if (passwordStoreItemDto == null)
                return BadRequest();

            var passwordStoreItem = passwordStoreItemDto.ToNonDto();
            passwordStoreItem.EncryptedPassword = PasswordEncryption.GetEncryptedPassword(passwordStoreItemDto.Password);

            _dbContext.PasswordStoreItems.Add(passwordStoreItem);
            _dbContext.SaveChanges();

            passwordStoreItemDto.Password = passwordStoreItem.EncryptedPassword;

            return CreatedAtRoute("GetPasswordStoreItem", new { id = passwordStoreItemDto.Id }, passwordStoreItemDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PasswordStoreItemDto> UpdatePasswordStoreItem([FromRoute] int id, [FromBody] PasswordStoreItemDto passwordStoreItemDto)
        {
            if (id == 0 || passwordStoreItemDto == null || id != passwordStoreItemDto.Id)
                return BadRequest();

            var passwordStoreItem = _dbContext.PasswordStoreItems.Find(id);

            if (passwordStoreItem == null)
                return NotFound();

            passwordStoreItemDto.MapValues(passwordStoreItem);
            passwordStoreItem.EncryptedPassword = PasswordEncryption.GetEncryptedPassword(passwordStoreItemDto.Password);

            _dbContext.SaveChanges();

            passwordStoreItemDto.Password = passwordStoreItem.EncryptedPassword;

            return Ok(passwordStoreItemDto);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePasswordStoreItem([FromRoute] int id)
        {
            if (id == 0)
                return BadRequest();

            var passwordStoreItem = _dbContext.PasswordStoreItems.Find(id);

            if (passwordStoreItem == null)
                return NotFound();

            _dbContext.PasswordStoreItems.Remove(passwordStoreItem);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
