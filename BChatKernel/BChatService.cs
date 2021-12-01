using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BChatKernel;

public sealed class BChatService
{
    //friens list
    private Dictionary<long, Friend> friends = new();
    //conmunication socket
    private SecureSocket.SecureSocket socket;

    public BChatService(BChatBuildConfig config)
    {
        this.Interface = config.bChatInterface;
        socket = new SecureSocket.SecureSocket(config.ip,config.port);


        
    }
    private IBChatInterface Interface;
    public void SendFriendMessage(Message message)
    {

    }

}

