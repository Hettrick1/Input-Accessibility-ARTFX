using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private Button[] menuBtns;
    [SerializeField] private InputActionReference menuRight;
    [SerializeField] private InputActionReference menuLeft;

    int index = 0;

    private void Start()
    {
        instance = this;
    }
    private void OnEnable()
    {
        menuLeft.action.performed += ChangeMenuLeft;
        menuRight.action.performed += ChangeMenuRight;
    }

    private void OnDisable()
    {
        menuLeft.action.performed -= ChangeMenuLeft;
        menuRight.action.performed -= ChangeMenuRight;
    }
    public void SetPause()
    {
        index = 0;
        if (PlayerMovement.instance.GetIsPaused() == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            menuBtns[index].Select();
            PlayerMovement.instance.SetIsPaused(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.instance.SetIsPaused(false);
        }
    }

    private void ChangeMenuLeft(InputAction.CallbackContext obj)
    {
        print("test");
        ChangeMenu(-1);
    }
    private void ChangeMenuRight(InputAction.CallbackContext obj)
    {
        print("test");
        ChangeMenu(1);
    }
    private void ChangeMenu(int menu)
    {
        if (index == 0 && menu < 0)
        {
            index = 2;
        }
        else if (index == 2 && menu > 0)
        {
            index = 0;
        }
        else
        {
            index += menu;
        }
        print(index);
        menuBtns[index].Select();
    }

    private void test()
    {
        print("qfpkig");
    }
}
