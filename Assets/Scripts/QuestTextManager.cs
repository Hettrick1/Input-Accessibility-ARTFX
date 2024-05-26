using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class QuestTextManager : MonoBehaviour
{
    [SerializeField] GameObject[] textsGO;

    private int activeGO;

    private string[,] texts = { 
        {"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. In hac habitasse platea dictumst. Nunc consequat interdum varius sit amet. Sed id semper risus in hendrerit gravida rutrum quisque non. Nam at lectus urna duis convallis. Ut porttitor leo a diam sollicitudin tempor id eu. Amet luctus venenatis lectus magna fringilla urna.",
            "Enim nulla aliquet porttitor lacus luctus accumsan tortor posuere ac. Malesuada fames ac turpis egestas maecenas pharetra convallis posuere. Varius quam quisque id diam vel quam elementum pulvinar. In pellentesque massa placerat duis ultricies lacus sed. Morbi tristique senectus et netus et malesuada fames." }, 
        { "Eget nullam non nisi est sit amet. Purus in mollis nunc sed id semper risus in. Nam libero justo laoreet sit amet cursus sit amet. Nisl pretium fusce id velit ut tortor pretium viverra. Turpis egestas maecenas pharetra convallis posuere.",
            "Duis ultricies lacus sed turpis tincidunt id aliquet risus. Libero nunc consequat interdum varius sit amet. In mollis nunc sed id semper risus in." }, 
        { "Amet risus nullam eget felis eget. Quam id leo in vitae turpis. Sed enim ut sem viverra. Sed egestas egestas fringilla phasellus. Consequat nisl vel pretium lectus quam id.",
            "Nec tincidunt praesent semper feugiat nibh sed. Purus semper eget duis at tellus at urna. Sed viverra tellus in hac habitasse platea dictumst vestibulum rhoncus." }, 
        { "Ridiculus mus mauris vitae ultricies leo integer malesuada nunc vel. In hendrerit gravida rutrum quisque non. Sed adipiscing diam donec adipiscing tristique risus. Id aliquet lectus proin nibh nisl condimentum id venenatis a.",
            "Posuere ac ut consequat semper viverra nam libero. Dictumst quisque sagittis purus sit amet volutpat consequat. Vel quam elementum pulvinar etiam non. Integer quis auctor elit sed vulputate mi sit amet." }, 
        { "Sed adipiscing diam donec adipiscing tristique. Erat velit scelerisque in dictum. Augue mauris augue neque gravida in fermentum. Nisl suscipit adipiscing bibendum est ultricies.",
            "Fames ac turpis egestas maecenas pharetra convallis. Neque gravida in fermentum et sollicitudin ac orci phasellus egestas. Posuere lorem ipsum dolor sit. " }, 
        { "Duis ut diam quam nulla porttitor massa id. A lacus vestibulum sed arcu non odio euismod. Accumsan lacus vel facilisis volutpat est velit egestas dui id. ",
            "Sit amet nulla facilisi morbi tempus iaculis. Felis eget velit aliquet sagittis id consectetur. Viverra orci sagittis eu volutpat odio facilisis mauris." }};

    public static QuestTextManager instance;

    int currentText = 0;
    private void Start()
    {
        instance = this;
        activeGO = 0;
        ResetQuests();
    }
    public void ResetQuests()
    {
        foreach (GameObject go in textsGO)
        {
            go.SetActive(false);
        }
        textsGO[activeGO].SetActive(true);
        textsGO[activeGO].GetComponent<TextMeshProUGUI>().text = texts[activeGO, 0];
    }
    public void SetText(int index)
    {
        currentText = 0;
        textsGO[activeGO].SetActive(false);
        textsGO[index].SetActive(true);
        activeGO = index;
        SetFirstText(currentText);
    }
    public void SetFirstText(int direction)
    {
        currentText = direction;
        if (!(currentText < 0 || currentText == 2)) // ca serait mieux de faire avec une liste par quetes
        {
            textsGO[activeGO].GetComponent<TextMeshProUGUI>().text = texts[activeGO, currentText];
        }
    }
}
