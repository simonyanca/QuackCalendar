using System.Threading.Tasks;
using QuackCalendar.Model;

namespace QuackCalendar.Service
{
    public interface IQCDomainFacade
    {
        Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest);

        Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest);
    }
}