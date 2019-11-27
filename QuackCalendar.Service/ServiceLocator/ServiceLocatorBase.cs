using QuackCalendar.Service.Manager;
using QuackCalendar.Service.Manager.Gateway;

namespace QuackCalendar.Service.ServiceLocator
{
    internal abstract class ServiceLocatorBase
    {
        internal QCServiceManager CreateServiceManager()
            => CreateServiceManagerCore();

        internal SqlGatewayBase CreateSqlGateway()
            => CreateSqlGatewayCore();

        protected abstract QCServiceManager CreateServiceManagerCore();
        protected abstract SqlGatewayBase CreateSqlGatewayCore();
    }
}