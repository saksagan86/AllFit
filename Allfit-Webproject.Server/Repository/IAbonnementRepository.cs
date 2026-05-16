using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IAbonnementRepository
    {
        Task AddAbonnementAsync(Abonnement abo);
        Task<Lid?> GetLid(string mollie);
    }
}
