using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Podcast.Mobile.Services
{
    public interface IStreamingService
    {
        Task PlayAsync(Uri fileUrl);
        Task PauseAsync();
        Task StopAsync();
    }
}
