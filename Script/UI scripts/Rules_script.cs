using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules_script : MonoBehaviour
{
    public bool firstPage;
    public static Rules_script[] pages = new Rules_script[2];  //0 - первая страница; 1 - вторая страница
        
    void Start()
    {
        if (firstPage)
            pages[0] = this;
        else
            pages[1] = this;
        gameObject.SetActive(false);
    }

    public void NextPage()
    {
        if (firstPage)
        {
            pages[1].gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            pages[0].gameObject.SetActive(true);
            pages[1].gameObject.SetActive(false);
        }
    }

    public static void OpenRules()
    {
        pages[0].gameObject.SetActive(true);
    }
    public static void CloseRules()
    {
        pages[0].gameObject.SetActive(false);
        pages[1].gameObject.SetActive(false);
    }
}
