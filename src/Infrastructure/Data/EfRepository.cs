﻿using Microsoft.EntityFrameworkCore;
using SupermarkerCheckout.Core.Entities;
using SupermarketCheckout.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SupermarkerCheckout.Infrastructure.Data
{
    /// <summary>
    /// "There's some repetition here - couldn't we have some the sync methods call the async?"
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly SupermarketContext _dbContext;

        public EfRepository(SupermarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        //public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = ApplySpecification(spec);
        //    return await specificationResult.ToListAsync();
        //}

        //public async Task<int> CountAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = ApplySpecification(spec);
        //    return await specificationResult.CountAsync();
        //}

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<T> FirstAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = ApplySpecification(spec);
        //    return await specificationResult.FirstAsync();
        //}

        //public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = ApplySpecification(spec);
        //    return await specificationResult.FirstOrDefaultAsync();
        //}

        //private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        //{
        //    var evaluator = new SpecificationEvaluator<T>();
        //    return evaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        //}
    }
}