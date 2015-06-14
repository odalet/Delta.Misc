using System;
using System.Collections.Generic;
using System.IO;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class InternalDirectoryParser : BaseInternalFileParser<InternalDirectory>
    {
        public InternalDirectoryParser(BinaryReader reader) : base(reader) { }
                
        protected override InternalDirectory ParseFileContent(InternalFileHeader internalFileHeader)
        {
            var directory = new InternalDirectory();         
            directory.BTreeHeader = ParseBTreeHeader();

            // Store the pages Data
            var pageBytes = new List<byte[]>();
            for (var i = 0; i < directory.BTreeHeader.TotalPages; i++)
            {
                var page = base.Reader.ReadBytes(directory.BTreeHeader.PageSize);
                pageBytes.Add(page);
            }

            if (directory.BTreeHeader.NLevels > 1)
                throw new NotSupportedException("B+ Trees with more than 1 level are not supported yet!");

            //for (var i = 1; i < Directory.BTreeHeader.NLevels; i++)
            //{
            //    // If we are here, this means NLevels > 1
            //    var pageBytes = Directory.BTreeHeader.Pages[Directory.BTreeHeader.RootPage];
            //}

            // Process the leaf-pages
            foreach (var pageData in pageBytes)
            {
                using (var mstream = new MemoryStream(pageData))
                using (var mreader = new BinaryReader(mstream))
                {
                    var leafPage = ProcessDirectoryLeafPage(mreader, pageData.Length);
                    directory.LeafPages.Add(leafPage);
                }

                // Now set the leaf pages previous/next relations
                foreach (var leafPage in directory.LeafPages)
                {
                    var nextIndex = leafPage.Header.NextPage;
                    if (nextIndex != -1)
                        leafPage.Next = directory.LeafPages[nextIndex];

                    var prevIndex = leafPage.Header.PreviousPage;
                    if (prevIndex != -1)
                        leafPage.Previous = directory.LeafPages[prevIndex];
                }
            }

            return directory;
        }
                
        protected override void Check(InternalDirectory result)
        {
            if (result.BTreeHeader.MustBeZero != (short)0)
                throw new WinHelpParsingException("Invalid BTree Header (MustBeZero != 0)");
            if (result.BTreeHeader.MustBeNegOne != (short)-1)
                throw new WinHelpParsingException("Invalid BTree Header (MustBeNegOne != -1)");
        }

        private BTreeHeader ParseBTreeHeader()
        {
            var header = new BTreeHeader();
            header.Magic = base.Reader.ReadUInt16();
            header.Flags = base.Reader.ReadUInt16();
            header.PageSize = base.Reader.ReadUInt16();
            header.Structure = Helper.DecodeStringz(base.Reader.ReadBytes(16));
            header.MustBeZero = base.Reader.ReadInt16();
            header.PageSplits = base.Reader.ReadInt16();
            header.RootPage = base.Reader.ReadInt16();
            header.MustBeNegOne = base.Reader.ReadInt16();
            header.TotalPages = base.Reader.ReadInt16();
            header.NLevels = base.Reader.ReadInt16();
            header.TotalBtreeEntries = base.Reader.ReadInt32();

            return header;
        }
        
        private BTreeLeafPage<DirectoryLeafEntry> ProcessDirectoryLeafPage(BinaryReader reader, int pageSize)
        {
            var leafPage = new BTreeLeafPage<DirectoryLeafEntry>();
            leafPage.Header = new BTreeNodeHeader();
            leafPage.Header.Unused = reader.ReadUInt16();
            leafPage.Header.NEntries = reader.ReadInt16();
            leafPage.Header.PreviousPage = reader.ReadInt16();
            leafPage.Header.NextPage = reader.ReadInt16();

            // Let's parse the entries
            for (int i = 0; i < leafPage.Header.NEntries; i++)
            {
                var entry = new DirectoryLeafEntry();
                entry.FileName = Helper.DecodeStringz(reader); // There is room for optimization here
                entry.FileOffset = reader.ReadInt32();
                leafPage.Entries.Add(entry);
            }

            return leafPage;
        }        
    }
}
