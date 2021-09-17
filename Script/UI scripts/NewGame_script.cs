using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame_script : BaseUI_script
{
    public GameObject[] plates;
    private void Start()
    {
        for (int i = 0; i < 3; i++)
            plates[i].SetActive(false);
        gameObject.SetActive(false);
    }
    public void ActivatePlate(int i)
    {
        plates[i].SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
