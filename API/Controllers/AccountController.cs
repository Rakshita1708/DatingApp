using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        //constructor //acceessing db through this
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        //http method post
        [HttpPost("register")] //POST:  localhost:5001/api/account/register

        //method register
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            //if the username already exists in the db 
            if(await UserExists(registerDto.Username)) return BadRequest("UserName is already taken");

            //randomly gernerating key for password
            using var hmac = new HMACSHA512(); // using key word is used when we dont need this class after it runs it saves space in memory of unused class

            var user = new AppUser
            {
                //column name "UserName" as in db and the passing parameter
                //saving our username in lowercase in db
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),//encrypting hashing algorithm HASHED
                PasswordSalt = hmac.Key //randomly generated key SALTED

            };
            //telling our entity framework that we want to add our user
            _context.Users.Add(user); //this just tracking our new entity in memory adding to our db

            await _context.SaveChangesAsync();

            return new  UserDto
            {
                //getting the username and token as oer userdto class
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        //login endpoint
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDto loginDto)
        {
            //getting the user from db 
            var user = await _context.Users.SingleOrDefaultAsync(x => 
            x.UserName == loginDto.Username);

            //unothorized method returns 401 if user is not in db
            if(user == null) return Unauthorized("Invalid UserName");
            
            //passing the salted key inside the compute function so that it generates the same hash when we first created   [reverse method]
            //the combination of the key and compute method exactly gives the same hash of our password 
            using var hmac = new HMACSHA512(user.PasswordSalt);//user.passwordsalt is the pass in db and it returns in binary format
            
            //loginDto.password is the password we are getting now while logging in
            var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //checking if the encrypted password and entered password is same?

            for(int i = 0; i < ComputedHash.Length; i++)
            {
                if( ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
             return new  UserDto
            {
                //getting the username and token as oer userdto class
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //checking if username exists
        private async Task<bool> UserExists(string username)
        {
            //x refers to a user in the table and AnyAsynch will loop over every user in the table 
            return await _context.Users.AnyAsync(x =>   x.UserName == username.ToLower());

        }
    }
}