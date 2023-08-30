using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

//using annotation
[ApiController]
[Route("api/[controller]")]  //localhost:5001/api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    //constructor
    public  UsersController(DataContext context)
    {
        _context = context;
    }

    //API Endpoint
    //http method get 
    [HttpGet]  //localhost:5001/api/users

    //returning a list of users in the table in json format 
    //converting it into asynchronous code [multithreading]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users =await _context.Users.ToListAsync();
        return users;
    }

    //returning a specific user id
    [HttpGet("{id}")] //localhost:5001/api/users/2

    public async Task<ActionResult<AppUser>> GetUsers(int id)
    {
        return await _context.Users.FindAsync(id);
        
    }
}
