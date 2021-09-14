using System.Collections.Generic;
using UnityEngine;

public class Cell_script : MonoBehaviour
{
    public GameObject num;
    public Sprite defaultSprite;
    public Sprite[] alphavitSprites;
    public static Cell_script[] cells = new Cell_script[169];    //все клетки на поле
    public int number;                           //номер клетки. нужен дл€ определени€ свободных соседей
    public Cell_script[] neighbors = new Cell_script[4];   //соседи
    public bool freedom, selected, attached;    //клетка без буквы, с незакрепленной буквой и с закрепленной буквой буквой соответственно
    public Letter_script attachedLetter;        //букввва на клетке 
    public bool doubleLetter, tripleLetter, doubleWord, tripleWord;   //бонусы

    private string alphavit = "абвгдежзийклмнопрстуфхцчшщъыьэю€";
    private int[] point = new int[32] { 1, 3, 2, 3, 2, 1, 5, 5, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 10, 5, 10, 5, 10, 10, 10, 5, 5, 10, 10, 3 };  //очки за буквы от а до € (без букввы Є)
    private Material mat;
    private SpriteRenderer sr;
    private bool isScaleUp, isScaleDown, showNumber;
    private int speed = 8;
    private float stage, start, end;

    void Start()
    {
        if (number == 84)
            freedom = true;
        cells[number] = this;
        mat = GetComponent<SpriteRenderer>().material;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = defaultSprite;
        Invoke("SetNeighbours", 1f);
    }

    public void SetNeighbours()
    {
        neighbors = new Cell_script[4];
        if (number > 0 && number % 13 != 0)
            neighbors[2] = cells[number - 1];   //0 - сверху
        if (number < 168 && number % 13 != 12)
            neighbors[0] = cells[number + 1];  //2 - снизу
        if (number > 12 && number / 13 != 0)
            neighbors[1] = cells[number - 13];   //1 - слева
        if (number <= 155 && number / 13 != 12)
            neighbors[3] = cells[number + 13];   //3 - справа
    }
    void Update()
    {
        if (isScaleUp)
        {
            stage += Time.deltaTime * speed;
            float scale = Mathf.Lerp(1, 1.2f, stage);
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
            float scale = Mathf.Lerp(1.2f, 1, stage);
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

    public static void ShowAvailable()
    {
        foreach (Cell_script cell in cells)
        {
            cell.Show(1);
        }
    }

    public static void StopShowAvailable()
    {
        foreach (Cell_script cell in cells)
        {
            cell.Show(0);
        }
    }

    private void Show(int n)
    {
        if (freedom && n == 1)
        {
            if (!GameManager_script.vertical && !GameManager_script.horizontal)   //если ориентаци€ слова не определена, то показать ввсе свободные
            {
                mat.SetFloat("_Streight", 0.25f * n);
            }
            else if (GameManager_script.vertical)   //иначе показать доступные вертикальному слову
            {
                List<Cell_script> word = GameManager_script.word;
                if (word.Count > 0)
                    if (this == word[0].neighbors[0] || this == word[word.Count - 1].neighbors[2])
                        mat.SetFloat("_Streight", 0.25f * n);
            }
            else if (GameManager_script.horizontal)    //иначе показать доступные горизонтальному слову
            {
                List<Cell_script> word = GameManager_script.word;
                if (word.Count > 0)
                    if (this == word[0].neighbors[1] || this == word[word.Count - 1].neighbors[3])
                        mat.SetFloat("_Streight", 0.25f * n);
            }
            else
                Debug.Log("error");
        }
        else                  //иначе не показывать свободные 
        {
            mat.SetFloat("_Streight", 0);
        }
    }

    public void SetLetter()
    {
        List<Cell_script> word = GameManager_script.word;
        bool placeIsRight = !GameManager_script.vertical && !GameManager_script.horizontal;   //определ€ет, можно ли поставить букву в этой клетке
        if (word != null && (GameManager_script.vertical || GameManager_script.horizontal))       //если ореинтаци€ слова не определена
            if (word.Count > 0)
            {
                placeIsRight = placeIsRight || (GameManager_script.vertical && (this == word[0].neighbors[0] || this == word[word.Count - 1].neighbors[2]));   //или слово вертикальное
                placeIsRight = placeIsRight || (GameManager_script.horizontal && (this == word[0].neighbors[1] || this == word[word.Count - 1].neighbors[3]));   //или слово горизонтальное
            }

        if (placeIsRight)
        {
            GameManager_script.AddInWord(this);
            freedom = false;
            selected = true;
            attachedLetter.Hide();
            int i = alphavit.IndexOf(attachedLetter.letter);
            if (i >= 0)
                sr.sprite = alphavitSprites[i];
            foreach (Cell_script cs in neighbors)
            {
                if (cs != null)
                    cs.SetStatus();
            }
            isScaleUp = true;
        }
        else  //если нельз€ сюда подставить букву, вернуть ее на каретку
        {
            attachedLetter.FindCellPlate();
            attachedLetter.Translate();
            attachedLetter = null;
        }
    }
    public void GetLetter()
    {
        GameManager_script.DeliteFromWord(this);
        freedom = true;
        selected = false;
        attachedLetter.Show();
        sr.sprite = defaultSprite;
        attachedLetter.FindCellPlate();
        attachedLetter.Translate();
        attachedLetter = null;
        foreach (Cell_script cs in neighbors)
        {
            if (cs != null)
                cs.SetStatus();
        }
    }
    public void ChangeLetter(Letter_script let)
    {
        attachedLetter.Show();
        attachedLetter.FindCellPlate();
        attachedLetter.Translate();
        attachedLetter = let;
        attachedLetter.Hide();
        int i = alphavit.IndexOf(attachedLetter.letter);
        if (i >= 0)
            sr.sprite = alphavitSprites[i];
    }
    public void SetStatus()  //ќпределить статус плиты на поле
    {
        bool verticalNeighbors = false, horizontalNeighbors = false;
        if (neighbors[0] != null)
            verticalNeighbors = neighbors[0].selected || neighbors[0].attached || verticalNeighbors;
        if (neighbors[2] != null)
            verticalNeighbors = verticalNeighbors || neighbors[2].selected || neighbors[2].attached;
        if (neighbors[3] != null)
            horizontalNeighbors = horizontalNeighbors || neighbors[3].selected || neighbors[3].attached;
        if (neighbors[1] != null)
            horizontalNeighbors = neighbors[1].selected || neighbors[1].attached || horizontalNeighbors;

        if (!selected && !attached)
        {
            if (verticalNeighbors && horizontalNeighbors)
                freedom = false;
            else if (verticalNeighbors || horizontalNeighbors)
                freedom = true;
            else
                freedom = false;
        }
    }
    public void SetPoints()
    {
        GameObject g = Instantiate(num, transform.position, Quaternion.identity);
        ShowNumber_script sh = g.GetComponent<ShowNumber_script>();
        if (doubleLetter)
        {
            sh.score = attachedLetter.points * 2;
            sh.multiplier *= 2;
        }
        else if (tripleLetter)
        {
            Debug.Log("triple");
            sh.score = attachedLetter.points * 3;
            sh.multiplier *= 3;
        }
        else
            sh.score = attachedLetter.points;

        if (doubleWord)
            ShowNumber_script.doubleWord = true;
        if (tripleWord)
            ShowNumber_script.tripleWord = true;
    }
}
