using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Model
{
    
    internal abstract class BaseModel<TKey>  
        where TKey : IComparable<TKey>, IComparable
    {
        public abstract TKey Id { get; set; }
    }
}
