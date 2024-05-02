using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSlider : MonoBehaviour
{
    [SerializeField] private GameObject btn;

    public void SelectFirstBtn()
    {
        btn.GetComponent<Slider>().Select();
    }
}
