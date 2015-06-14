using System.IO;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class SystemFileParser : BaseInternalFileParser<SystemFile>
    {
        private const short systemFileHeaderMagic = 0x036C;
        public SystemFileParser(BinaryReader reader) : base(reader) { }

        protected override SystemFile ParseFileContent(InternalFileHeader internalFileHeader)
        {
            var header = new SystemHeader();

            header.Magic = base.Reader.ReadInt16();
            header.Minor = base.Reader.ReadInt16();
            header.Major = base.Reader.ReadInt16();
            header.GenDate = Helper.DecodeDate(base.Reader.ReadInt32());
            header.Flags = base.Reader.ReadUInt16();
            var file = new SystemFile() { SystemHeader = header };
            return file;
        }
        
        protected override void Check(SystemFile result)
        {
            if (result.SystemHeader.Magic != systemFileHeaderMagic)
                throw new WinHelpParsingException("Invalid |SYSTEM Header: Invalid Magic number");

        }
    }
}
