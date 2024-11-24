using UnityEngine;

public class MultimeterModel
{
    private const float Resistance = 1000f; // Ом
    private const float Power = 400f; // Вт
    
    public enum Mode
    {
        Resistance,
        Current,
        VoltageAC,
        VoltageDC
    }
    
    public Mode CurrentMode { get; private set; } = Mode.Resistance;
    
    public void SetMode(Mode mode)
    {
        CurrentMode = mode;
    }
    
    public (Mode, float) GetDisplayValue()
    {
        float value = 0;
        switch (CurrentMode)
        {
            case Mode.Resistance:
                value = Resistance;
                break;
            case Mode.Current:
                value = CalculateCurrent();
                break;
            case Mode.VoltageDC:
                value = CalculateVoltage(CalculateCurrent());
                break;
            case Mode.VoltageAC:
                value = .01f; // Условное значение для переменного тока
                break;
        }
        
        return (CurrentMode, value);
    }
    
    private float CalculateCurrent()
    {
        return Mathf.Sqrt(Power / Resistance); // A = ?(P/R)
    }
    
    private float CalculateVoltage(float current)
    {
        return current * Resistance; // V = A * R
    }
}