using DatingApp.API.Data;
using DatingApp.API.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository: IAuthRepository
    {
        private readonly DataContext _context;
     
     public AuthRepository(DataContext context )
     {
         _context=context;
     }

 public async Task<User> Register(User user, string password)
 {
      byte[] passwordhash,passwordsalt;
      CreatePassword(password, out passwordhash, out passwordsalt);
      user.PasswordHash= passwordhash;
      user.PasswordSalt=passwordsalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
      return user;
      
 }

 public void CreatePassword(string password ,out byte[] passwordhash, out byte[] passwordsalt)
 {

       using(var hmac=new System.Security.Cryptography.HMACSHA512())
       {
           passwordsalt =hmac.Key;
           passwordhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
       }

 }



 public async Task<User> Login(string username,string password)
 {
     var user =await _context.Users.FirstOrDefaultAsync(x =>x.Username==username);
     if(user==null)
     return null;

     if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
     return null;

     return user;
  
  }

  public bool VerifyPasswordHash(string password ,byte[] passwordhash, byte[] passwordsalt)
  {
       using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordsalt))
       {
          
           var computedhash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           for(int i=0;i<computedhash.Length;i++)
           {
               if(computedhash[i]!=passwordhash[i])
               return false;
           }
       }
       return true;

  }

 public async Task<bool> UserExist(string username)
{

    if(await _context.Users.AnyAsync(x=> x.Username==username))
     return true;
     return false;
}
         
    }
}