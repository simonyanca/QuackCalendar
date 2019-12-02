using System.Threading.Tasks;
using QuackCalendar.Model;

namespace QuackCalendar.Service
{
    public interface IQCDomainFacade
    {
        Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest);

        Task<QCDeleteEventResponse> DeleteEventAsync(QCDeleteEventRequest qcDeleteEventRequest);

        Task<QCGetEventResponse> GetEventAsync(QCGetEventRequest qcGetEventRequest);

        Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest);

        Task<QCUpdateEventResponse> UpdateEventAsync(QCUpdateEventRequest qcUpdateEventRequest);
    }
}