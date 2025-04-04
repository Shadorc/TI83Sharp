namespace TI83Sharp;

public class SortA : SortCommand
{
    public SortA() : base("SortA(")
    {
    }

    protected override Comparison<int> Compare(TiList list)
    {
        return (i, j) => list[i].CompareTo(list[j]);
    }
}
