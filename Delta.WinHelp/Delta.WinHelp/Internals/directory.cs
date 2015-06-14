using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.WinHelp.Internals
{

    internal class InternalDirectory : InternalFile
    {
        public InternalDirectory()
        {
            IndexPages = new List<BTreeIndexPage<DirectoryIndexEntry>>();
            LeafPages = new List<BTreeLeafPage<DirectoryLeafEntry>>();
        }

        public BTreeHeader BTreeHeader { get; set; }

        public IList<BTreeIndexPage<DirectoryIndexEntry>> IndexPages { get; set; }
        public IList<BTreeLeafPage<DirectoryLeafEntry>> LeafPages { get; set; }
    }

    internal class DirectoryIndexEntry : BTreeIndexEntry
    {
        // TODO
    }


    internal class DirectoryLeafEntry : BTreeLeafEntry
    {
        public string FileName { get; set; }
        public int FileOffset { get; set; }
    }
}
