using BuildingBlocks;

namespace TopUp.Domain;
//AED 5, AED 10, AED 20, AED 30, AED 50, AED 75, AED 100
public class TopUpAmount : Enumeration<TopUpAmount, decimal>
{
    public static readonly TopUpAmount AED5 = new(5, "AED 5");
    public static readonly TopUpAmount AED10 = new(10, "AED 10");
    public static readonly TopUpAmount AED20 = new(20, "AED 20");
    public static readonly TopUpAmount AED30 = new(30, "AED 30");
    public static readonly TopUpAmount AED50 = new(50, "AED 50");
    public static readonly TopUpAmount AED75 = new(75, "AED 75");
    public static readonly TopUpAmount AED100 = new(100, "AED 100");

    protected TopUpAmount(decimal value, string name)
        : base(value, name)
    {
    }
}
