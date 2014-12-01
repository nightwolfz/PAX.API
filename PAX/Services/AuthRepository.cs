using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PAX.Models;

namespace PAX.Services
{
    // https://github.com/tjoudeh/AngularJSAuthentication/blob/master/AngularJSAuthentication.API/AuthRepository.cs
    public class AuthRepository : IDisposable
    {
        private ConnectionContext _context;
        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _context = new ConnectionContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
        }

        public AuthRepository(ConnectionContext context)
        {
            _context = context;
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
        }

        public async Task<IdentityUser> FindUser(string userName, string password) => await _userManager.FindAsync(userName, password);

        public Client FindClient(string clientId) => _context.Clients.Find(clientId);

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _context.RefreshTokens.SingleOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null) await RemoveRefreshToken(existingToken);
            
            _context.RefreshTokens.Add(token);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
            return await _context.SaveChangesAsync() > 0;
        }

        public List<RefreshToken> GetAllRefreshTokens() => _context.RefreshTokens.ToList();
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId) => await _context.RefreshTokens.FindAsync(refreshTokenId);
        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo) => await _userManager.FindAsync(loginInfo);
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login) => await _userManager.AddLoginAsync(userId, login);

        public async Task<IdentityResult> CreateAsync(IdentityUser user)
        {
            var account = await _userManager.CreateAsync(user);
            var profile = new Profile()
            {
                ProfileId = user.Id,
                Name = user.UserName
            };
            _context.Profiles.Add(profile);
            return account;
        }

        public IdentityResult Create(IdentityUser user, Profile profile)
        {
            var account = _userManager.Create(user);
            profile.ProfileId = user.Id;
            profile.Name = user.UserName;
            _context.Profiles.Add(profile);
            return account;
        }


        public void Dispose()
        {
            _context.Dispose();
            _userManager.Dispose();
        }
    }
}