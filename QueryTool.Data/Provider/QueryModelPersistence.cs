using QueryTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Provider
{
    internal abstract class QueryModelPersistence : IModelPersistence<Query, int>
    {
        
        public abstract int Save(Query model);

        public abstract void Edit(int id, Query updatedModel);

        public abstract void Delete(int id);

        public abstract Query GetById(int id);

        public abstract IEnumerable<Query> GetAll();
        
    }
}
