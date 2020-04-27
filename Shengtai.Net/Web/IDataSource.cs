using System.Text;

namespace Shengtai.Web
{
    public interface IErrorDataSource : IDataSource
    {
        string ErrorMessage { get; }
    }

    public interface IDataSource
    {
        string ToString();

        StringBuilder AppendLine(string value);
    }
}