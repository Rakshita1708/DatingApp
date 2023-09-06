using API.Entities;

namespace API.Interface
{
    public interface ITokenService
    {
        //creating a method CreateToken passing APPUSER CLASS as an argument and called user
        /* This class assigns as a contract between our interface and our implementation any other class that implements this interface has to support this mthod and 
        it has to return a string  and has to take APPUSER as an argument*/
        string CreateToken(AppUser user);
    }
}