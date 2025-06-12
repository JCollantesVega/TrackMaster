using System.Threading.Tasks;
using TrackMaster.Core.Models;

namespace TrackMaster.Core.Services.Persistence
{
    public interface ISessionUploader
    {
        Task UploadSessionAsync(Session session);
    }
}
