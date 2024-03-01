using Assistant.Wpf.Models;
using System.Threading.Tasks;

namespace Assistant.Wpf.Services
{
    internal interface ISettingService
    {
        Task<Settings> GetSettings();

        Task SetSettings(Settings settings);
    }
}
