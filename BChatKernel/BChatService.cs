using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BChatKernel;

public sealed class BChatService
{
    private Thread thread;
    //friens list
    private Dictionary<long, Friend> friends = new();
    //conmunication socket
    private SecureSocket.SecureSocket socket;
    BChatBuildConfig config;

    public BChatService(BChatBuildConfig config)
    {
        this.Interface = config.bChatInterface;
        this.config = config;

    }
    public void Stop()
    {
        thread.Interrupt();
        Interface.onConnectionLost(null,ErrorType.Cancelled);
    }
    /// <summary>
    /// to connect to server and start listening
    /// </summary>
    public void Connect()
        
    {
        thread=new Thread(() =>
        {
            try
            {
                socket = new SecureSocket.SecureSocket(config.ip, config.port);
            }
            catch (Exception e)
            {
                Interface.onConnectionLost(e,ErrorType.Net);
                thread.Interrupt();
            }
        });
        thread.Start();

        
    }
    private IBChatInterface Interface;
    public void SendFriendMessage(Message message)
    {

    }

}

