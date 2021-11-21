using System.Threading.Tasks;
using Yabe.Domain;

namespace Yabe.Ui.Managers
{
    public interface IBlobManager
    {
        Task<BlobDirectory> ListBlobs(string directory);
        Task Delete(string name);
        Task Upload(Models.Upload uploadBlob);
    }
}
