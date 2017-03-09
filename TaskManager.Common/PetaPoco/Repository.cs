using System;
using System.Collections.Generic;
using PetaPoco;

namespace TaskManager.Common.PetaPoco
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ContextDb _db;

        protected ContextDb Db
        {
            get
            {
                if (_db == null)
                {
                    return new ContextDb();
                }
                return this._db;
            }
        }

        protected ContextDb CreateDao(string connStr)
        {
            this._db=new ContextDb(connStr);
            return this.Db;
        }
        public int Delete(TEntity entity)
        {
           return Db.Delete<TEntity>(entity);
        }

        public int DeleteByEntityId(object primaryKey)
        {
            return Db.Delete<TEntity>(primaryKey);
        }

        public bool Exists(object primaryKey)
        {
            return this.Db.Exists<TEntity>(primaryKey);
        }

        public TEntity Get(object primaryKey)
        {
            return this.Db.SingleOrDefault<TEntity>(primaryKey);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.GetAll(null);
        }

        public IEnumerable<TEntity> GetAll(string orderBy)
        {
            PocoData data = PocoData.ForType(typeof(TEntity),null);
            Sql sql = Sql.Builder.Select(new object[] { data.TableInfo.PrimaryKey }).From(new object[] { data.TableInfo.TableName });
            if (!string.IsNullOrEmpty(orderBy))
            {
                sql.OrderBy(new object[] { orderBy });
            }
            return this.Db.Fetch<TEntity>(sql);
        }

        public object Insert(TEntity entity)
        {
            return this.Db.Insert(entity);
        }

        public IEnumerable<TEntity> PopulateEntitiesByEntityIds<T>(IEnumerable<T> entityIds)
        {
            Sql sql = Sql.Builder;
            
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            this.Db.Update(entity);
        }
    }
}
