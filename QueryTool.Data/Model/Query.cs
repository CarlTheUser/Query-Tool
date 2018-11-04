using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTool.Data.Model
{
    internal class Query : BaseModel<int>
    {

        public override int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string Provider { get; set; }

    }
}
