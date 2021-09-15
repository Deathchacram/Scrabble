using UnityEngine;
using UnityEngine.UI;

public class Score_script : MonoBehaviour
{
    public static Score_script[] scorePlates = new Score_script[4];
    public int number;
    public Vector3 pointsPos;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public bool isHuman;
    [HideInInspector]
    public bool hasParent = false;

    [SerializeField]
    private Image[] scoreImages;  //0 - тип игрока; 1, 2, 3 - имя; 4, 5, 6 - кол-во очков; 9 - точка
                                  //костыльный метод отображения очков через картинки. Потом исправить
    [SerializeField]
    private Sprite[] sprites;    //0-31 буквы. 32-41 цифры. 42 пустое поле. 43 человек. 44 ИИ
    private string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя0123456789 ";
    private bool isScaleUp, isScaleDown;
    private float speed = 8, stage;

    void Awake()
    {
        scorePlates[number] = this;
        foreach (Image im in scoreImages)
        {
            im.color = new Color(0, 0, 0, 0);
        }
    }

    private void Start()
    {
        Invoke("Set", 0.1f);
    }
    private void Update()
    {
        if (isScaleUp)
        {
            stage += Time.deltaTime * speed;
            float scale = Mathf.Lerp(1, 1.1f, stage);
            transform.localScale = new Vector3(scale, scale, 0);
            if (stage > 1)
            {
                stage = 0;
                isScaleUp = false;
                isScaleDown = true; ;
            }
        }
        else if (isScaleDown)
        {
            stage += Time.deltaTime * speed;
            float scale = Mathf.Lerp(1.1f, 1, stage);
            transform.localScale = new Vector3(scale, scale, 0);
            if (stage > 1)
            {
                stage = 0;
                isScaleDown = false;
            }
        }
        else
        {
            stage = 0;
            isScaleUp = false;
            isScaleDown = false;
        }
    }

    private void Set()
    {
        pointsPos = Cursor_script.cam.ScreenToWorldPoint(scoreImages[9].rectTransform.position);
    }

    public void UpdateScore(int score)  //без хардкода не получилось обойтись
    {
        if (isHuman)
            scoreImages[0].sprite = sprites[43];
        else
            scoreImages[0].sprite = sprites[44];
        for (int i = 1; i < 4; i++)
        {
            int n = alphabet.IndexOf(playerName[i - 1]);
            scoreImages[i].sprite = sprites[n];
        }
        string s = score.ToString();
        for (int i = 4; i < 7; i++)
        {
            if (i - 4 < s.Length)
            {
                int n = alphabet.IndexOf(s[s.Length - i + 3]);
                scoreImages[i].sprite = sprites[n];
            }
            else
                scoreImages[i].sprite = sprites[42];
        }
        scoreImages[7].sprite = sprites[42];

        for (int i = 0; i < 9; i++)
        {
            scoreImages[i].color = new Color(1, 1, 1, 1);
        }
        isScaleUp = true;
    }
}
