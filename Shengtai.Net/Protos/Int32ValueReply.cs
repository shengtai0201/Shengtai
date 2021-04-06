using Google.Protobuf.WellKnownTypes;
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
    public partial class Int32ValueReply : IExtensible
    {
        private IExtension _extensionData;
        public IExtension GetExtensionObject(bool createIfMissing) => Extensible.GetExtensionObject(ref _extensionData, createIfMissing);

        [ProtoMember(1, Name = @"result")]
        public Int32Value Result { get; set; }

        [ProtoMember(2, Name = @"message")]
        [DefaultValue("")]
        public string Message { get; set; } = "";

        [ProtoMember(3, Name = @"time", DataFormat = DataFormat.WellKnown)]
        public DateTime? Time { get; set; }

    }
}
