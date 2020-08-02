using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recommendSongsService.API.models.dto;
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
            // TODO Criptografar senha e notes
            // Logar request - data - user - message
            User userToSave = new User
            {
                Name = user.Name,
                Password = user.Password,
                Email = user.Email,
                Hometown = user.Hometown
            };
            var userSaved = _dbContext.Users.Add(userToSave);
            await _dbContext.SaveChangesAsync();
            
            // Save Personal Notes
            var notes = from note in user.PersonalNotes
                        select new Note
                        {
                            NoteData = note.NoteData,
                            UserId = userSaved.Entity.Id
                        };
            _dbContext.Notes.AddRange(notes.Cast<Note>().ToList());
            await _dbContext.SaveChangesAsync();
            return userSaved.Entity;
        }
    }
}