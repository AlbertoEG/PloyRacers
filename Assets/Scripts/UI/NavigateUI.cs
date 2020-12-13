using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class NavigateUI : MonoBehaviour
{
    //INPUTS
    private PlayerInputActions inputActions;

    //BUTTON LIST & OTHER RELATED VARIABLES
    public Button[] buttonList;

    private int currentButton;
    
    private void OnEnable()
    {
        inputActions.Enable();
        currentButton = 0;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        inputActions = new PlayerInputActions();

        inputActions.PlayerControls.NavigateMenu.performed += ctx => selectNextButton(ctx.ReadValue<Vector2>().y);

        currentButton = 0;
        buttonList[currentButton].Select();
    }

    public void selectNextButton(float _i)
    {
        Debug.Log(_i);
        if (currentButton + _i >= 0 && currentButton + _i <= buttonList.Length - 1)
        {
            currentButton -= (int) _i;
            buttonList[currentButton].Select();
        }
    }
}
