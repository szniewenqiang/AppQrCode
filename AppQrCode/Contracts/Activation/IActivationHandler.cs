using System.Threading.Tasks;

namespace AppQrCode.Contracts.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle();

        Task HandleAsync();
    }
}
