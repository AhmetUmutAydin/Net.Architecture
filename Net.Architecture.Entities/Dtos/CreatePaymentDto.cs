using Net.Architecture.Entities.BaseEntities;

namespace Net.Architecture.Entities.Dtos
{
    public class CreatePaymentDto : IDTO
    {
        public long PackageType { get; set; }
        public long PackageDay { get; set; }
        public bool MemberConfirmationPayment { get; set; }
    }
}
