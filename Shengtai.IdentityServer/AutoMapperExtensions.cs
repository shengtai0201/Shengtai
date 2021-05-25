using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class AutoMapperExtensions
    {
        public static TDestination ChangeType<TSource, TDestination>(this AutoMapper.IMapper mapper, TSource source)
        {
            if (source.GetType() == typeof(TDestination))
                return (TDestination)Convert.ChangeType(source, typeof(TDestination));

            return mapper.Map<TSource, TDestination>(source);
        }
    }
}
