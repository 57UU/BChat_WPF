using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BChatKernel;

namespace WPF_Interface;

public class Assets
{
    public static Assets assets=new Assets();
    private Assets() { }
    public BChatService kernel;
    public IBChatInterface chatInterface;
}

