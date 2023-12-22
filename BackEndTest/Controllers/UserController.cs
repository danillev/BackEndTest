using BackEndTest.Data;
using BackEndTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly UserGenericRepository _userRepository;


        public UserController(ApplicationContext context)
        {
            _context = context;
            _userRepository = new UserGenericRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(users);

        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, user.PasswordHash);
            _userRepository.Create(user);
            _userRepository.Save();
            return Ok(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!_context.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }
            _userRepository.Update(user);
            _userRepository.Save();
            return Ok(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Delete(user);
            _userRepository.Save();
            return Ok(user);
        }
    }
}
