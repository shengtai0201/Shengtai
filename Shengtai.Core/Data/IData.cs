namespace Shengtai.Data
{
    public interface IData
    {
        DataStatus DataStatus { get; set; }
        int? DataErrorCount { get; set; }
    }
}
