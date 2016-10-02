using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.Domain.Impl
{
    public class ValidationException: Exception
    {
        internal ValidationException(ModelBase model, string field, string msg = null)
            :base(BuildMsg(model, field, msg))
        {
        }

        private static string BuildMsg(ModelBase model, string field, string msg = null)
        {
            return string.Format("Field '{0}' in '{1}' has invalid value. {2}",
               field, model.GetType().Name, msg);
        }
    }
}
