namespace TI83Sharp;

public class SortD : SortCommand
{
    public SortD() : base("SortD(")
    {
    }

    protected override Comparison<int> Compare(TiList list)
    {
        return (i, j) => list[j].CompareTo(list[i]);
    }
}
