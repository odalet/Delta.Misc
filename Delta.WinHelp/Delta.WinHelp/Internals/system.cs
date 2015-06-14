using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.WinHelp.Internals
{
    internal class SystemFile : InternalFile
    {
        public SystemFile()
        {
            Records = new List<SystemRec>();
        }

        public SystemHeader SystemHeader { get; set; }
        public IList<SystemRec> Records { get; private set; }
    }

    internal class SystemHeader
    {
        public short Magic { get; set; }
        public short Minor { get; set; }
        public short Major { get; set; }
        public DateTime? GenDate { get; set; }
        public ushort Flags { get; set; }
        public string HelpFileTitle { get; set; }
    }

    internal class SystemRec
    {
        public ushort RecordType { get; set; }
        public ushort DataSize { get; set; }
        public byte[] Data { get; set; }
    }
}
