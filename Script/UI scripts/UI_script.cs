using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_script : MonoBehaviour
{
    public Image nextTurnImag, backgroun, settingsMenu, doesNotExist;
    public static Image background;
    public static UI_script singleton;

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
        singleton = this;
        background = backgroun;
        scoreImages = scoreImage;
        sprites = sprite;
        Hide();
    }

    public static void NextTurn()
    {
        singleton.nextTurnImag.gameObject.SetActive(true);
        background.enabled = true;
        Cursor_script.ignoreInput = true;
        string nam = GameManager_script.players[GameManager_script.turn].playerName;
        for (int i = 0; i < 3; i++)
        {
            int n = alphabet.IndexOf(nam[i]);
            Debug.Log(n);
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
        singleton.nextTurnImag.gameObject.SetActive(false);
    }

    public void Settings()
    {
        settingsMenu.gameObject.SetActive(true);
        Cursor_script.ignoreInput = true;
        background.enabled = true;
    }
    public void DoesntExist()
    {
        background.enabled = true;
        doesNotExist.gameObject.SetActive(true);
    }
    public void DoesntExistDisable()
    {
        background.enabled = false;
        doesNotExist.gameObject.SetActive(false);
    }
    public static void OpenUrl()
    {
        Application.OpenURL("https://github.com/Deathchacram");
    }
    public static void Load()
    {
        foreach(Player_script ps in GameManager_script.players)
        {
            ps.name = "aaa";
            ps.EndGame();
            ps.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
