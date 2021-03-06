namespace BChatKernel;
public record JsonBase
{
    public string type { get; set; }
    public string text { get; set; }
    public long id { get; set; }

}
public record LoginInfo : JsonBase
{
    
    public string pass { get; set; }
    public string token { get; set; }
    public string mac { get; set; }
    public bool isUseToken()
    {
        return token != null || mac != null;
    }
}
public record MessageInfo:JsonBase
{
    
}
public record Respond : JsonBase
{
    public string result { get; set; }
}