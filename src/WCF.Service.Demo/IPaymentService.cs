using System.ServiceModel;

namespace WCF.Service.Demo
{
    [ServiceContract]
    public interface IPaymentService
    {
        [OperationContract]
        void Process(string client);
    }
}
