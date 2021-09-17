using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu_script : MonoBehaviour
{
    public GameObject dictionary, support, exit, newGame;
    public bool MainMenu;

    [SerializeField]
    private Sprite[] sprites;  //0 - проверять; 1 - не проверять
    private Image dictionarySprite;

    void Start()
    {
        dictionarySprite = dictionary.GetComponent<Image>();
        if (!MainMenu)
            gameObject.SetActive(false);
    }

    public void OpenSupport()
    {
        support.SetActive(true);
    }

    public void Exit()
    {
        exit.gameObject.SetActive(true);
    }
    public void CloseApp()
    {
        Application.Quit();
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    public void Dictionary()
    {
        if (GameManager_script.dictionaryCheck)
        {
            GameManager_script.dictionaryCheck = false;
            dictionarySprite.sprite = sprites[1];
        }
        else
        {
            GameManager_script.dictionaryCheck = true;
            dictionarySprite.sprite = sprites[0];
        }
    }
    private void OnDisable()
    {
        try
        {
            UI_script.background.enabled = false;
            Cursor_script.ignoreInput = false;
        }
        catch(Exception ex) { }
    }

    public void NewGameMenu()
    {
        newGame.gameObject.SetActive(true);
    }
}
