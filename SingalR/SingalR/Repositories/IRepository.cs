using SingalR.Models;
using SingalR.Models.ViewModels;

namespace SingalR.Repositories
{
    public interface IRepository<TModel, TGraphModel>
    {
        public Task<IEnumerable<TModel>> GetItems();
        public Task<TModel> GetItemDetails(int ItemId);

        public Task<List<TGraphModel>> GetItemGraphData();
    }
}
