using LoopMainProject.Common.ViewModels;
using LoopMainProject.Model.Entities;


namespace LoopMainProject.Business.Contract
{
    public interface IBaseBusiness<T> where T : BaseEntity
    {
        Task<SamanSalamatResponse> CreateAsync(T t, CancellationToken cancellationToken);

  ///*      Task<SamanSalamatResponse> CreateUserAsync(ApplicationUser user, CancellationToken cancellationToke*/);
    }
}
