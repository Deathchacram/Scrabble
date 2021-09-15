using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI_script : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
