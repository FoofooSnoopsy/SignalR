using SingalR.Models;
using SingalR.Models.ViewModels;

namespace SingalR.Repositories
{
    public interface IRepository<TModel, TModelGraphData>
    {
        public Task<IEnumerable<TModel>> GetList();
        public Task<TModel> GetFromId(int productId);

        public Task<List<TModelGraphData>> GetGraphData();
    }
}
