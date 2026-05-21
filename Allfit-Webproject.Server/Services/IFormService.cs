namespace Allfit_Webproject.Server.Services
{
    public interface IFormService
    {
        Task SaveFormAsync(Dtos.IntakeDTO formData);
    }
}
