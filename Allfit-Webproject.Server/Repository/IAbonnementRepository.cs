using Allfit_Webproject.Server.Models;

namespace Allfit_Webproject.Server.Repository
{
    public interface IAbonnementRepository
    {
        /// <summary>
        /// Sla een nieuw abonnement op in de database.
        /// </summary>
        Task AddAbonnementAsync(Abonnement abo);
        /// <summary>
        /// Zoek een lid op basis van Mollie ID van een betaling/abonnement.
        /// </summary>
        Task<Lid?> GetByMollieIDAsync(string mollie);
        /// <summary>
        /// Werk de gegevens van een lid bij in de database.
        /// </summary>
        Task UpdateLidAsync(Lid lid);
    }
}
