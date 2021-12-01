using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel
{
    internal class Assets
    {
        public Assets assets=new Assets();
        private Assets() { }
        public Hashtable<int, Friend> friends = new();
    }
}
