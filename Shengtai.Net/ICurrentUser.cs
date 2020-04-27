namespace Shengtai
{
    public interface ICurrentUser<in TPrincipal>
    {
        TPrincipal CurrentUser { set; }
    }
}