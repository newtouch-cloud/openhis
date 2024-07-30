using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    public class DecimalPrecisionAttributeConvention: PrimitivePropertyAttributeConfigurationConvention<DecimalPrecisionAttribute>

    {

        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DecimalPrecisionAttribute attribute)

        {

            if (attribute.Precision < 1 || attribute.Precision > 38)

            {

                throw new InvalidOperationException("Precision must be between 1 and 38.");

            }

            if (attribute.Scale > attribute.Precision)

            {

                throw new InvalidOperationException("Scale must be between 0 and the Precision value.");

            }

            configuration.HasPrecision(attribute.Precision, attribute.Scale);

        }

    }
}
