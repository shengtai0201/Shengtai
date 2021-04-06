using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Protos
{
    [ProtoContract]
    public partial class BoolReply : IExtensible
    {
        private IExtension _extensionData;
        public IExtension GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref _extensionData, createIfMissing);

        [ProtoMember(1, Name = @"result")]
        public bool Result { get; set; }

        [ProtoMember(2, Name = @"message")]
        [DefaultValue("")]
        public string Message { get; set; } = "";

        [ProtoMember(3, Name = @"time", DataFormat = DataFormat.WellKnown)]
        public DateTime? Time { get; set; }
    }
}
