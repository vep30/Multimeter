using UnityEngine;
using UnityEngine.InputSystem;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private MultimeterView view;
    [SerializeField] private Color highlightColor;
    
    private InputControls inputControls;
    private MultimeterModel model;
    private Color originalColor;
    private bool isActive;
    
    public void Start()
    {
        originalColor = renderer.material.color;
        inputControls = new InputControls();
        model = new MultimeterModel();
        inputControls.Enable();
        UpdateDisplay();
        inputControls.ScrollMap.ScrollAction.performed += MouseScroll;
    }
    
    private void OnDisable()
    {
        inputControls.ScrollMap.ScrollAction.performed -= MouseScroll;
        inputControls.Disable();
    }
    
    private void MouseScroll(InputAction.CallbackContext context)
    {
        float scroll = context.ReadValue<float>();
        if (scroll == 0 || !isActive)
            return;
        
        bool next = scroll < 0f;
        transform.Rotate(0,0, next ? 90 : -90);
        model.SetMode(GetMode(model.CurrentMode, next));
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
    
    private MultimeterModel.Mode GetMode(MultimeterModel.Mode currentMode, bool isNext)
    {
        switch (currentMode)
        {
            case MultimeterModel.Mode.Resistance:
                return isNext ? MultimeterModel.Mode.Current : MultimeterModel.Mode.VoltageDC;
            case MultimeterModel.Mode.Current:
                return isNext ? MultimeterModel.Mode.VoltageAC : MultimeterModel.Mode.Resistance;
            case MultimeterModel.Mode.VoltageAC:
                return isNext ? MultimeterModel.Mode.VoltageDC : MultimeterModel.Mode.Current;
            case MultimeterModel.Mode.VoltageDC:
                return isNext ? MultimeterModel.Mode.Resistance : MultimeterModel.Mode.VoltageAC;
            default:
                return isNext ? MultimeterModel.Mode.Resistance : MultimeterModel.Mode.VoltageDC;
        }
    }
    
    private void UpdateDisplay()
    {
        view.UpdateDisplay(model.GetDisplayValue());
    }
}