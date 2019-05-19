using System.Threading.Tasks;

namespace G19.Models.Repositories {
    public interface IMailRepository {
        Task<bool> SendMailAsync(string comment, int oefId);
        
    }
}
