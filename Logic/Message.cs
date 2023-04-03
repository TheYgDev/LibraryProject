using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [Serializable]
    public struct Message
    {
        public string Title;
        public string Info;
        public Message(string title,string info)
        {
            Title = title;
            Info = info;
        }
        public override string ToString()
        {
            return Title;
        }
        public string MessageInfo()
        {
            return Info;
        }
    }
}
