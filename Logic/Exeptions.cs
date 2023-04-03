using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{

    [Serializable]
    public class ItemNotFoundException : Exception
    {

        public ItemNotFoundException() : base("Item Not Found") { }

        public ItemNotFoundException(string message) : base(message) { }

    }

    [Serializable]
    public class ItemBorrowedException : Exception
    {
        public ItemBorrowedException() : base("Item Is Already Borrowed") { }
        public ItemBorrowedException(string message) : base(message) { }

    }
    [Serializable]
    public class ItemNotBorrowedException : Exception
    {
        public ItemNotBorrowedException() : base("Item Is Not Borrowed") { }
        public ItemNotBorrowedException(string message) : base(message) { }

    }
    [Serializable]
    public class DuplicationException : Exception
    {
        public DuplicationException() : base("Item Duplicated") { }
        public DuplicationException(string message) : base(message) { }

    }
}
