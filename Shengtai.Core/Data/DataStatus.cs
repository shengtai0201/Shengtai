namespace Shengtai.Data
{
    public enum DataStatus : int
    {
        None = 0,

        // 資料已準備完畢
        DataReady = 1,

        // 資料已傳送成功
        Transmitted = 2
    }
}
