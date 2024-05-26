using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private Button btn;

    public void SelectFirstBtn()
    {
        if(Gamepad.current != null)
        {
            btn.Select();
        }
    }
}
