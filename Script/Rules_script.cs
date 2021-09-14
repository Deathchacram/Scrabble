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
    }

    public void NextPage()
    {
        if (firstPage)
        {
            pages[1].enabled = true;
            enabled = false;
        }
        else
        {
            pages[0].enabled = true;
            pages[1].enabled = false;
        }
    }

    public static void OpenRules()
    {
        pages[0].enabled = true;
    }
    public static void CloseRules()
    {
        pages[0].enabled = false;
        pages[1].enabled = false;
    }
}
