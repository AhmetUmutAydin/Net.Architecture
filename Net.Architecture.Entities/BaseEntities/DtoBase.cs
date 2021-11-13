namespace Net.Architecture.Entities.BaseEntities
{
    public abstract class DtoBase<TPrimary> : IDTO
    {
        public TPrimary Id { get; set; }
    }

    public abstract class DtoBase : DtoBase<long?>
    {
        public DtoBase() : base()
        {

        }
    }
}
