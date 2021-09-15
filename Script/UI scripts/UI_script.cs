using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_script : MonoBehaviour
{
    public GameObject settingsMenu;
    public Image nextTurnImag, backgroun, rules; 
    public static GameObject settingsMenuStatic;
    public static Image nextTurnImage, background;

    [SerializeField]
    private Image[] scoreImage;
    [SerializeField]
    private static Image[] scoreImages;
    [SerializeField]
    private Sprite[] sprite;    //0-31 буквы. 32-41 цифры. 42 пустое поле. 43 человек. 44 ИИ
    [SerializeField]
    private static Sprite[] sprites;    //0-31 буквы. 32-41 цифры. 42 пустое поле. 43 человек. 44 ИИ
    private static string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя0123456789 ";

    void Start()
    {
        settingsMenuStatic = settingsMenu;
        nextTurnImage = nextTurnImag;
        background = backgroun;
        scoreImages = scoreImage;
        sprites = sprite;
        Hide();
    }

    public static void  NextTurn()
    {
        nextTurnImage.gameObject.SetActive(true);
        background.enabled = true;
        Cursor_script.ignoreInput = true;
        for (int i = 0; i < 3; i++)
        {
            int n = alphabet.IndexOf(GameManager_script.players[GameManager_script.turn].playerName[i]);
            scoreImages[i].sprite = sprites[n];
            scoreImages[i].color = new Color(1, 1, 1, 1);
        }
    }

    public void Hide()
    {
        background.enabled = false;
        Cursor_script.ignoreInput = false;
        for (int i = 0; i < 3; i++)
        {
            scoreImages[i].sprite = null;
            scoreImages[i].color = new Color(0, 0, 0, 0);
        }
        nextTurnImage.gameObject.SetActive(false);
    }

    public void Settings()
    {
        settingsMenuStatic.gameObject.SetActive(true);
        Cursor_script.ignoreInput = true;
        background.enabled = true;
    }
    public static void OpenUrl()
    {
        Application.OpenURL("https://github.com/Deathchacram");
    }
    public static void Load()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
