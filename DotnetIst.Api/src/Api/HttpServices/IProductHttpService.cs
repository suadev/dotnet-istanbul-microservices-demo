using System.Collections.Generic;
using System.Threading.Tasks;


namespace Api.HttpServices
{
    public interface IProductHttpService
    {
        Task<object> GetList();
    }
}