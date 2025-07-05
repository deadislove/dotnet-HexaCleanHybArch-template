using Adapters.User.Domain.Interfaces;
using Adapters.User.Infra.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HexaCleanHybArch.Template.Config.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Users = Adapters.User.Domain.DTOs.User;

namespace Adapters.User.Infra.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper) { 
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(Users user)
        {
            UserEntity entity = _mapper.Map<UserEntity>(user);
            await _dbContext.Set<UserEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<UserEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            List<Users> users = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .ProjectTo<Users>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return users ?? [];
        }

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(s => s.Id.Equals(id));

            return entity == null ? null : _mapper.Map<Users>(entity);
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .Where(w =>
                    w.Email.Equals(email)
                    )
                .FirstOrDefaultAsync();

            return entity == null ? null : _mapper.Map<Users>(entity);
        }

        public async Task<Users?> GetByEmailAndPasswordAsync(string email, string password)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .Where(w =>
                    w.Email.Equals(email) &&
                    w.Password.Equals(password)
                    )
                .FirstOrDefaultAsync();

            return entity == null ? null : _mapper.Map<Users>(entity);
        }

        public async Task UpdateAsync(Users user)
        {
            UserEntity? entity = await _dbContext.Set<UserEntity>()
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(s => s.Id.Equals(user.Id));

            if (entity != null)
            {

                _mapper.Map(user, entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
