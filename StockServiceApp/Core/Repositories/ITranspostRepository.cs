using TransportServiceApp.Core.Models;

namespace TransportServiceApp.Core.Repositories
{
    public interface ITranspostRepository
    {
        Task<Transpost> Add(Transpost transpost);
        Task<Transpost> Remove(Transpost transpost);
        
    }
}
