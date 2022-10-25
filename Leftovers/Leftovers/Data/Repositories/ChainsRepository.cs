using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leftovers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leftovers.Data.Repositories
{
    public interface IChainsRepository
    {
        Task DeleteAsync(Chain chain);
        Task<List<Chain>> GetAsync();
        Task<Chain> GetAsync(int chainId);
        Task InsertAsync(Chain chain);
        Task UpdateAsync(Chain chain);
    }

    public class ChainsRepository : IChainsRepository
    {
        private readonly LeftoversContext _leftoversContext;
        public ChainsRepository(LeftoversContext leftoversContext)
        {
            _leftoversContext = leftoversContext;
        }

        public async Task<List<Chain>> GetAsync()
        {
            return await _leftoversContext.Chains.ToListAsync();

        }
        public async Task<Chain> GetAsync(int chainId)
        {
            return await _leftoversContext.Chains.FirstOrDefaultAsync(o => o.Id == chainId);
        }

        public async Task InsertAsync(Chain chain)
        {
            _leftoversContext.Chains.Add(chain);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Chain chain)
        {
            _leftoversContext.Chains.Update(chain);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Chain chain)
        {
            _leftoversContext.Chains.Remove(chain);
            await _leftoversContext.SaveChangesAsync();
        }
    }
}
