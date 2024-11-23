using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private MultimeterView view;
    [SerializeField] private Renderer renderer;
    [SerializeField] private Color highlightColor;
    
    private bool isActive;
    private Color originalColor;
    private MultimeterModel model;

    public void Start()
    {
        originalColor = renderer.material.color;
        model = new MultimeterModel();
        UpdateDisplay();
    }
    
    public void OnMouseEnter()
    {
        isActive = true;
        renderer.material.color = highlightColor;
    }
    
    public void OnMouseExit()
    {
        isActive = false;
        renderer.material.color = originalColor;
    }
    
    public void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll == 0 || !isActive)
            return;

        if (scroll > 0f)
            GoNextMode();
        else if (scroll < 0f)
            GoPreviousMode();
        
        UpdateDisplay();
    }
    
    private void GoNextMode()
    {
        transform.Rotate(0,0,90);
        model.SetMode(GetNextMode(model.CurrentMode));
    }
    
    private void GoPreviousMode()
    {
        transform.Rotate(0,0,-90);
        model.SetMode(GetPreviousMode(model.CurrentMode));
    }
    
    private MultimeterModel.Mode GetNextMode(MultimeterModel.Mode currentMode)
    {
        switch (currentMode)
        {
            case MultimeterModel.Mode.Resistance:
                return MultimeterModel.Mode.Current;
            case MultimeterModel.Mode.Current:
                return MultimeterModel.Mode.VoltageAC;
            case MultimeterModel.Mode.VoltageAC:
                return MultimeterModel.Mode.VoltageDC;
            case MultimeterModel.Mode.VoltageDC:
                return MultimeterModel.Mode.Resistance;
            default:
                return MultimeterModel.Mode.Resistance;
        }
    }
    
    private MultimeterModel.Mode GetPreviousMode(MultimeterModel.Mode currentMode)
    {
        switch (currentMode)
        {
            case MultimeterModel.Mode.VoltageDC:
                return MultimeterModel.Mode.VoltageAC;
            case MultimeterModel.Mode.VoltageAC:
                return MultimeterModel.Mode.Current;
            case MultimeterModel.Mode.Current:
                return MultimeterModel.Mode.Resistance;
            case MultimeterModel.Mode.Resistance:
                return MultimeterModel.Mode.VoltageDC;
            default:
                return MultimeterModel.Mode.VoltageDC;
        }
    }
    
    private void UpdateDisplay()
    {
        view.UpdateDisplay(model.GetDisplayValue());
    }
}