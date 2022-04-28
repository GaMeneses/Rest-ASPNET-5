using RestASPNET.Data.VO;
using RestASPNET.Model;
using RestASPNET.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestASPNET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => (u.UserName == userName));
        }
        public bool RevokeToken(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => (u.UserName == userName));
            if (user == null)return false;

            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            var result = _context.Users.FirstOrDefault(p => p.Id == user.Id);
            
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                return null;
            }          
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider Algorithm)
        {
            return BitConverter.ToString(Algorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
    }
}
