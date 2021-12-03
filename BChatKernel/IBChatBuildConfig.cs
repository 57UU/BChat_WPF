using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

public record BChatBuildConfig
{
    public string password { get; set; }
    public string username { get; set; }
    public IBChatInterface bChatInterface { get; set; }
    public string ip { get; set; }
    public int port { get; set; }
}

