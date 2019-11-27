using QuackCalendar.Service.Manager;
using QuackCalendar.Service.Manager.Gateway;

namespace QuackCalendar.Service.ServiceLocator
{
    internal sealed class MySqlServiceLocator : ServiceLocatorBase
    {
        protected override QCServiceManager CreateServiceManagerCore()
            => new QCServiceManager(this);

        protected override SqlGatewayBase CreateSqlGatewayCore()
            => new MySqlGateway();
    }
}