using QueryTool.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Provider
{
    internal abstract class SqlConfigurationModelPersistence : IModelPersistence<SqlConfiguration, int>
    {

        public abstract int Save(SqlConfiguration model);

        public abstract void Edit(int id, SqlConfiguration updatedModel);

        public abstract void Delete(int id);

        public abstract SqlConfiguration GetById(int id);

        public abstract IEnumerable<SqlConfiguration> GetAll();


        
    }
}
