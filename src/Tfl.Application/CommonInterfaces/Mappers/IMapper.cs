namespace Tfl.Application.CommonInterfaces.Mappers
{
    public interface IMapper<Tfrom, Tto>
    {
        Tto Map(Tfrom source);
    }
}
