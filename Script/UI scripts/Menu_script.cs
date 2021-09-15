using UnityEngine;
using UnityEngine.UI;

public class Menu_script : BaseUI_script
{
    public GameObject dictionary, support, exit;

    [SerializeField]
    private Sprite[] sprites;  //0 - проверять; 1 - не проверять
    private Image dictionarySprite;

    void Start()
    {
        dictionarySprite = dictionary.GetComponent<Image>();
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
        UI_script.background.enabled = false;
        Cursor_script.ignoreInput = false;
    }
}
