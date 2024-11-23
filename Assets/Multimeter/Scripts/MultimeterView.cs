using TMPro;
using UnityEngine;

public class MultimeterView : MonoBehaviour
{
    [Header("UI Text")]
    [SerializeField] private TextMeshProUGUI resistance;
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI DCVoltage;
    [SerializeField] private TextMeshProUGUI ACVoltage;
    
    [Header("Display Text")]
    [SerializeField] private TextMeshProUGUI displayText;
    
    public void UpdateDisplay((MultimeterModel.Mode curMode, float value) info)
    {
        string formattedValue = info.value.ToString("F2");
        displayText.text = formattedValue;
        SetAllZero();
        SetNeedMode(info.curMode, formattedValue);
    }
    
    private void SetAllZero()
    {
        resistance.text = "0";
        current.text = "0";
        DCVoltage.text = "0";
        ACVoltage.text = "0";
    }
    
    private void SetNeedMode(MultimeterModel.Mode curMode, string value)
    {
        switch (curMode)
        {
            case MultimeterModel.Mode.Resistance:
                resistance.text = value;
                break;
            case MultimeterModel.Mode.Current:
                current.text = value;
                break;
            case MultimeterModel.Mode.VoltageDC:
                DCVoltage.text = value;
                break;
            case MultimeterModel.Mode.VoltageAC:
                ACVoltage.text = value;
                break;
        }
    }
}