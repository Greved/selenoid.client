using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Selenoid.Client.Models;

namespace Selenoid.Client
{
    public interface ISelenoidVideoClient
    {
        Task<List<SelenoidListItem>> GetAsync();
        Task<Stream> GetAsync(string videoName);
        Task DeleteAsync(string videoName);
    }
}