using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private Button[] menuBtns;
    [SerializeField] private GameObject[] menuSlots;
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
            ColorBlock cb = menuBtns[index].colors;
            cb.normalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f, 1);
            menuBtns[index].colors = cb;
            menuSlots[index].SetActive(true);
            menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
            PlayerMovement.instance.SetIsPaused(true);
        }
        else
        {
            ColorBlock cb = menuBtns[index].colors;
            cb.normalColor = Color.white;
            menuBtns[index].colors = cb;
            menuSlots[index].SetActive(false);
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
        ColorBlock cb = menuBtns[index].colors;
        cb.normalColor = Color.white;
        menuBtns[index].colors = cb;
        menuSlots[index].SetActive(false);
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
        menuSlots[index].SetActive(true);
        cb = menuBtns[index].colors;
        cb.normalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f, 1);
        menuBtns[index].colors = cb;
        menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
    }
    public void ChangeMenuSlot(int menu)
    {
        menuSlots[index].SetActive(false);
        ColorBlock cb = menuBtns[index].colors;
        cb.normalColor = Color.white;
        menuBtns[index].colors = cb;
        index = menu;
        menuSlots[index].SetActive(true);
        cb = menuBtns[index].colors;
        cb.normalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f, 1);
        menuBtns[index].colors = cb;
        menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
    }

    private void test()
    {
        print("qfpkig");
    }
}