
using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProuductAgg;

public class ProductImage : BaseEntity
{
    public long ProductId { get; internal set; }
    public string ImageName { get; private set; }
    public int Sequence { get; private set; }

    public ProductImage(string imageName, int sequence)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
        Sequence = sequence;
    }
}
