using System.Threading.Tasks;
using QuackCalendar.Model;
using QuackCalendar.Service.Manager;
using QuackCalendar.Service.ServiceLocator;

namespace QuackCalendar.Service
{
    public sealed class QCDomainFacade : IQCDomainFacade
    {
        private readonly ServiceLocatorBase serviceLocator;
        private QCServiceManager qcServiceManager;

        private QCServiceManager QCServiceManager
            => qcServiceManager ?? (qcServiceManager = serviceLocator.CreateServiceManager());

        public QCDomainFacade() : this(new MySqlServiceLocator())
        {
        }

        internal QCDomainFacade(ServiceLocatorBase serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public async Task<QCAddEventResponse> AddEventAsync(QCAddEventRequest qcAddEventRequest)
            => await QCServiceManager.AddEventAsync(qcAddEventRequest);

        public async Task<QCGetEventsResponse> GetEventsAsync(QCGetEventsRequest qcGetEventsRequest)
            => await QCServiceManager.GetEventsAsync(qcGetEventsRequest);
    }
}