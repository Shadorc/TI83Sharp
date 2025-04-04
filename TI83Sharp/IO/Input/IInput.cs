namespace TI83Sharp;

public interface IInput
{
    public int GetKey();

    public void OnKeyPressed(Keys keys);

    public char WaitChar();
}
