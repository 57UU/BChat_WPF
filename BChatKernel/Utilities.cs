using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BChatKernel;

internal class Utilities
{
    private Utilities()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// to parse a string to json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="msg"></param>
    /// <returns>json class</returns>
    /// <exception cref="JsonReaderException">when a incorrect string is parsed</exception>
    public static T ParseStringToJson<T>(string msg) where T : JsonBase
    {
        return JsonConvert.DeserializeObject<T>(msg);
    }
    public static string ParseJsonToString(object o)
    {
        return JsonConvert.SerializeObject(o);
    }
    public static void  Main()
    {
        MessageInfo messageInfo = new() { id=123,text="h",type=MessageTypes.msg.ToString()};
        string s=ParseJsonToString(messageInfo);
        Console.WriteLine(s);
        Console.WriteLine(ParseStringToJson<JsonBase>(s));
        
    }
}
