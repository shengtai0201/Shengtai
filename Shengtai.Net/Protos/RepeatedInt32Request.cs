using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Protos
{
    [ProtoContract]
    public partial class RepeatedInt32Request : IExtensible
    {
        private IExtension _extensionData;
        public IExtension GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref _extensionData, createIfMissing);

        [ProtoMember(1, Name = @"values", IsPacked = true)]
        public int[] Values { get; set; }
    }
}
