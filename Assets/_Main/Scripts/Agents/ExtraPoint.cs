public class ExtraPoint
{
    double _value = 0f;
    public double Value
    {
        get
        {
            var val = _value;
            _value = 0f;
            return val;
        }
        set => _value = value;
    }
}