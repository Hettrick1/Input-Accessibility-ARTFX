using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] private Button[] menuBtns;
    [SerializeField] private GameObject[] menuSlots;
    [SerializeField] private GameObject[] settingsMenuSlots;
    [SerializeField] private Toggle fullscreenToggle, vSyncToggle;
    [SerializeField] private InputActionReference menuRight;
    [SerializeField] private InputActionReference menuLeft;
    [SerializeField] private InputActionReference returnBack;

    int index = 0;
    int settingIndex = 0;

    int isFullScreenActivated, isVSyncActivated;

    private void Start()
    {
        instance = this;
        isFullScreenActivated = PlayerPrefs.GetInt("fullscreen", 1);
        if (isFullScreenActivated == 1)
        {
            fullscreenToggle.isOn = true;
        }
        else if (isFullScreenActivated == 0)
        {
            fullscreenToggle.isOn = false;
        }
        isVSyncActivated = PlayerPrefs.GetInt("vsync", 1);
        if (isVSyncActivated == 1)
        {
            vSyncToggle.isOn = true;
        }
        else if (isVSyncActivated == 0)
        {
            vSyncToggle.isOn = false;
        }
    }
    private void OnEnable()
    {
        menuLeft.action.performed += ChangeMenuLeft;
        menuRight.action.performed += ChangeMenuRight;
        returnBack.action.performed += ReturnBack;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        menuLeft.action.performed -= ChangeMenuLeft;
        menuRight.action.performed -= ChangeMenuRight;
        returnBack.action.performed -= ReturnBack;
        InputSystem.onDeviceChange -= OnDeviceChange;
        PlayerPrefs.SetInt("fullscreen", isFullScreenActivated);
        PlayerPrefs.SetInt("vsync", isVSyncActivated);
    }
    public void SetPause(InputAction.CallbackContext context)
    {
        if (context.performed && PlayerMovement.instance.GetIsPaused() == false)
        {
            index = 0;
            transform.GetChild(0).gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ColorBlock cb = menuBtns[index].colors;
            cb.normalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f, 1);
            menuBtns[index].colors = cb;
            menuSlots[index].SetActive(true);
            if(Gamepad.current != null)
            {
                menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
            }
            PlayerMovement.instance.SetIsPaused(true);
        }
        else if (context.performed && PlayerMovement.instance.GetIsPaused() == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            ColorBlock cb = menuBtns[index].colors;
            cb.normalColor = Color.white;
            foreach (Button btn in menuBtns)
            {
                btn.colors = cb;
            } 
            foreach (GameObject menu in menuSlots)
            {
                menu.SetActive(false);
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.instance.SetIsPaused(false);
        }
    }

    private void ReturnBack(InputAction.CallbackContext obj)
    {
        if(PlayerMovement.instance.GetIsPaused() == true)
        {
            if (index == 1 && Gamepad.current != null)
            {
                menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
            }
        }
    }

    private void ChangeMenuLeft(InputAction.CallbackContext obj)
    {
        if (PlayerMovement.instance.GetIsPaused() == true)
        {
            ChangeMenu(-1);
        }
    }
    private void ChangeMenuRight(InputAction.CallbackContext obj)
    {
        if (PlayerMovement.instance.GetIsPaused() == true)
        {
            ChangeMenu(1);
        }
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
        if(index == 1)
        {
            settingsMenuSlots[0].SetActive(true);
            if(Gamepad.current != null)
            {
                settingsMenuSlots[0].GetComponent<SelectSlider>().SelectFirstBtn();
            }
        }
        cb = menuBtns[index].colors;
        cb.normalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f, 1);
        menuBtns[index].colors = cb;
        if (Gamepad.current != null)
        {
            menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
        }
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
        if(Gamepad.current != null)
        {
            menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
        }
    }
    public void ChangeSettingsSlots(int settings)
    {
        settingsMenuSlots[settingIndex].SetActive(false);
        settingIndex = settings;
        settingsMenuSlots[settingIndex].SetActive(true);
    }
    public void setFullscreen()
    {
        if (fullscreenToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            isFullScreenActivated = 1;
        }

        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            isFullScreenActivated = 0;
        }
    }
    public void setVsync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            isFullScreenActivated = 1;
        }

        else
        {
            QualitySettings.vSyncCount = 0;
            isFullScreenActivated = 0;
        }
    }
    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        {
            if (change == InputDeviceChange.Added && Gamepad.current != null)
            {
                menuSlots[index].GetComponent<SelectButton>().SelectFirstBtn();
            }
            else if (change == InputDeviceChange.Removed && Gamepad.current == null)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

}
