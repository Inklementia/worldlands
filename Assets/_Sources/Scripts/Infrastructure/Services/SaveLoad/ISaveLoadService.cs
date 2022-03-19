using _Sources.Scripts.Data;

namespace _Sources.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}