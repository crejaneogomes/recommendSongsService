using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
using recommendSongsService.API.Utils;
using recommendSongsService.Model;

namespace recommendSongsService.API.Service
{
    public class UserService : IUserService
    {
        private readonly RecommendSongsDbContext _dbContext;
        public UserService(RecommendSongsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> SaveUSer(UserDTO user)
        {
            User userToSave = new User
            {
                Name = user.Name,
                Password = UtilsFunctions.HashValue(user.Password),
                Email = user.Email,
                Hometown = user.Hometown
            };
            var userSaved = _dbContext.Users.Add(userToSave);
            await _dbContext.SaveChangesAsync();
            
            // Save Personal Notes
            var notes = from note in user.PersonalNotes
                        select new Note
                        {
                            NoteData = UtilsFunctions.HashValue(note.NoteData),
                            UserId = userSaved.Entity.Id
                        };
            _dbContext.Notes.AddRange(notes.Cast<Note>().ToList());
            await _dbContext.SaveChangesAsync();
            return userSaved.Entity;
        }

        public Dictionary<string, string> ForgotUserPassword(string email)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Random rnd = new Random();
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return null;
            } else
            {
                user.ForgotPasswordCode = rnd.Next(10000, 99999).ToString();
                user.Password = null;
                _dbContext.SaveChanges();
                result.Add("Code", user.ForgotPasswordCode);
                result.Add("User Email", user.Email);
                return result;
            }
        }

        public Dictionary<string, string> ChangeUserPassword(ResetPasswordDTO user)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            var userToUpdate = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email && x.ForgotPasswordCode == user.ForgotPasswordCode);
            if (user == null)
            {
                return null;
            } else
            {
                userToUpdate.Password = UtilsFunctions.HashValue(user.NewPassword);
                _dbContext.SaveChanges();
                result.Add("Result","Password updated");
                return result;
            }
        }
    }
}