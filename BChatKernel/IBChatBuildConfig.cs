using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

public record BChatBuildConfig
{
    public string password;
    public int username;
    public IBChatInterface bChatInterface;
    public string ip;
    public int port;
}

