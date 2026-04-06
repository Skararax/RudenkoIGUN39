
public class Frame 
{
    public int firstThrow = 0;
    public int secondThrow = 0;

    public bool isStrike => firstThrow == 10;
    public bool isSpare => !isStrike && firstThrow + secondThrow == 10;
}
