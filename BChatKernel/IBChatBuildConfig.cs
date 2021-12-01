using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

internal interface IBChatBuildConfig
{
    public string password { get; set; }
    public int username { get; set; }

}

