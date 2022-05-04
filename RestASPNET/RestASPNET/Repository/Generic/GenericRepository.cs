using Microsoft.EntityFrameworkCore;
using RestASPNET.Model.Base;
using RestASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestASPNET.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected MySQLContext _context;
        private DbSet<T> _dataSet;
        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dataSet = _context.Set<T>();
        }

        public List<T> FindAll()
        {
           return _dataSet.ToList();
        }

        public T FindByID(long id)
        {
            return _dataSet.FirstOrDefault(p => p.Id == id);
        }
        public T Create(T item)
        {
            try
            {
                _dataSet.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Update(T item)
        {
            var result = _dataSet.FirstOrDefault(p => p.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
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
        public void Delete(long id)
        {
            var result = _dataSet.FirstOrDefault(p => p.Id == id);
            if (result != null)
            {
                try
                {
                    _dataSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exits(long id)
        {
            return _dataSet.Any(p => p.Id == id);
        }

        public List<T> FindWithPagedSearch(string query)
        {
            return _dataSet.FromSqlRaw<T>(query).ToList();
        }

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                result = command.ExecuteScalar().ToString();
            }

            return int.Parse(result);
        }
    }
}
