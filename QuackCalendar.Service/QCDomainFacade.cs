using QuackCalendar.Model;
using QuackCalendar.Service.Manager;
using QuackCalendar.Service.ServiceLocator;

namespace QuackCalendar.Service
{
    public sealed class QCDomainFacade
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

        public QCAddEventResponse AddEvent(QCAddEventRequest qcAddEventRequest)
            => QCServiceManager.AddEvent(qcAddEventRequest);

        public QCGetEventsResponse GetEvents(QCGetEventsRequest qcGetEventsRequest)
            => QCServiceManager.GetEvents(qcGetEventsRequest);
    }
}