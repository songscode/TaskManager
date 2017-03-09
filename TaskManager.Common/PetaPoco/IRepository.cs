using System.Collections.Generic;

namespace TaskManager.Common.PetaPoco
{
    public interface IRepository<TEntity> where TEntity : class
    {
        int Delete(TEntity entity);
        ///<summary>
        ///从数据库删除实体(by 主键)
        ///</summary>
        ///<param name="primaryKey">主键</param>
        ///<returns></returns>
        int DeleteByEntityId(object primaryKey);
        ///<summary>
        ///依据主键检查实体是否存在于数据库
        ///</summary>
        ///<param name="primaryKey">主键</param>
        bool Exists(object primaryKey);
        ///<summary>
        ///依据主键获取单个实体
        ///</summary>
        ///<remarks>
        ///自动对实体进行缓存（除非实体配置为不允许缓存）
        ///</remarks>
        TEntity Get(object primaryKey);
        ///<summary>
        ///获取所有实体（仅用于数据量少的情况）
        ///</summary>
        ///<remarks>
        ///自动对进行缓存（缓存策略与实体配置的缓存策略相同）
        ///</remarks>
        IEnumerable<TEntity> GetAll();
        ///<summary>
        ///获取所有实体（仅用于数据量少的情况）
        ///</summary>
        ///<param name="orderBy">排序字段（多个字段用逗号分隔）</param>
        ///<remarks>
        ///自动对进行缓存（缓存策略与实体配置的缓存策略相同）
        ///</remarks>
        IEnumerable<TEntity> GetAll(string orderBy);
        object Insert(TEntity entity);
        IEnumerable<TEntity> PopulateEntitiesByEntityIds<T>(IEnumerable<T> entityIds);
        void Update(TEntity entity);
    }
}
