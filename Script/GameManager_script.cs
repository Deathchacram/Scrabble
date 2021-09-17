using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_script : MonoBehaviour
{
    public static int turn; //номер игрока, который сейчас ходит
    public static bool dictionaryCheck = true;      //проверка по словарю
    public static bool endingTurn;
    public static bool vertical = false, horizontal = false;    //ореинтация слова. нужна для определения доступных полей и формирования самого слова
    public static List<Player_script> players = new List<Player_script>();
    public static List<Cell_script> word = new List<Cell_script>();  //текущее слово  

    [SerializeField]
    private Sprite[] endButton;
    [SerializeField]
    private TextAsset[] dictionary;
    [SerializeField]
    private Image endButtonImage;
    private string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";
    private static GameManager_script gm;
    private static Cell_script coreLetter;


    void Start()
    {
        turn = 0;
        players = new List<Player_script>();
        word = new List<Cell_script>();
        coreLetter = null;
        Debug.Log("start");
        Invoke("PlayerSettings", 0.1f);
        gm = this;
    }
    private void PlayerSettings()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in player)
        {
            if (p.active)
                p.GetComponent<Player_script>().StartGame();
        }
        for (int i = 1; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                players[i].TurnEnd();
            }
        }
    }

    public static void AddInWord(Cell_script letter)
    {
        if (letter.neighbors[0] != null)                                                            //проверка ориентации слова
            vertical = (letter.neighbors[0].selected || letter.neighbors[0].attached || vertical) && !horizontal;
        if (letter.neighbors[2] != null)
            vertical = (vertical || letter.neighbors[2].selected || letter.neighbors[2].attached) && !horizontal;
        if (letter.neighbors[3] != null)
            horizontal = (horizontal || letter.neighbors[3].selected || letter.neighbors[3].attached) && !vertical;
        if (letter.neighbors[1] != null)
            horizontal = (letter.neighbors[1].selected || letter.neighbors[1].attached || horizontal) && !vertical;


        if (!vertical && !horizontal)
        {
            word.Add(letter);
            coreLetter = letter;
        }
        else if (vertical)
        {
            try
            {
                if (letter.neighbors[2].attachedLetter != null && letter.neighbors[0].attachedLetter == null)      //удачи разобраться в этом
                {
                    word.Insert(0, letter);

                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[2]) == -1)
                        AddInWord(letter.neighbors[2]);
                }
                else if (letter.neighbors[2].attachedLetter != null)
                {

                    if (word.IndexOf(letter.neighbors[0]) == -1)
                    {
                        word.Insert(0, letter);
                        AddInWord(letter.neighbors[0]);
                    }
                    else
                        word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[2]) == -1)
                        AddInWord(letter.neighbors[2]);
                }
                else if (letter.neighbors[0].attachedLetter != null)
                {
                    if (word.IndexOf(letter.neighbors[0]) == -1 && word.IndexOf(letter) == -1)
                        word.Insert(0, letter);
                    else
                        word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[0]) == -1)
                        AddInWord(letter.neighbors[0]);
                }
            }
            catch (Exception ex)
            {
                if (letter.neighbors[2] == null)
                {
                    word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[0]) == -1 && letter.neighbors[0].attachedLetter != null)
                        AddInWord(letter.neighbors[0]);
                }
                else
                {
                    word.Insert(0, letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[2]) == -1 && letter.neighbors[2].attachedLetter != null)
                        AddInWord(letter.neighbors[2]);
                }
            }
            /*}
            else if (letter.neighbors[2] != null)
            {
                word.Add(letter);
                if (letter.attached)
                    coreLetter = letter;
                if (word.IndexOf(letter.neighbors[2]) == -1)
                    AddInWord(letter.neighbors[2]);
            }
            else
            {
                word.Insert(0, letter);
                if (letter.attached)
                    coreLetter = letter;
                if (word.IndexOf(letter.neighbors[0]) == -1)
                    AddInWord(letter.neighbors[0]);
            }*/
        }
        else if (horizontal)
        {
            try
            {
                if (letter.neighbors[3].attachedLetter != null && letter.neighbors[1].attachedLetter == null)
                {
                    word.Insert(0, letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[3]) == -1)
                        AddInWord(letter.neighbors[3]);
                }
                else if (letter.neighbors[3].attachedLetter != null)
                {
                    if (word.IndexOf(letter.neighbors[1]) == -1)
                    {
                        word.Insert(0, letter);
                        AddInWord(letter.neighbors[1]);
                    }
                    else
                        word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[3]) == -1)
                        AddInWord(letter.neighbors[3]);
                }
                else if (letter.neighbors[1].attachedLetter != null)
                {
                    if (word.IndexOf(letter.neighbors[1]) == -1 && word.IndexOf(letter) == -1)
                        word.Insert(0, letter);
                    else
                        word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[1]) == -1)
                        AddInWord(letter.neighbors[1]);
                }
            }
            catch (Exception ex)
            {
                if (letter.neighbors[3] == null)
                {
                    word.Add(letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[1]) == -1 && letter.neighbors[1].attachedLetter != null)
                        AddInWord(letter.neighbors[1]);
                }
                else
                {
                    word.Insert(0, letter);
                    if (letter.attached)
                        coreLetter = letter;
                    if (word.IndexOf(letter.neighbors[3]) == -1 && letter.neighbors[3].attachedLetter != null)
                        AddInWord(letter.neighbors[3]);
                }
            }
            /*}
            else if (letter.neighbors[3] != null)
            {
                word.Add(letter);
                if (letter.attached)
                    coreLetter = letter;
                if (word.IndexOf(letter.neighbors[3]) == -1)
                    AddInWord(letter.neighbors[3]);
            }
            else
            {
                word.Insert(0, letter);
                if (letter.attached)
                    coreLetter = letter;
                if (word.IndexOf(letter.neighbors[1]) == -1)
                    AddInWord(letter.neighbors[1]);
            }*/
        }


        /*string wordString = "";

        foreach (Cell_script cs in word)
        {
            wordString += cs.attachedLetter.letter;
        }

        Debug.Log(wordString);*/
        gm.ButtonUpdate();
    }

    public static void DeliteFromWord(Cell_script letter)
    {
        if (letter == coreLetter)
        {
            int i = 0;
            while (word.Count != 1)
            {
                if (word[i] != coreLetter)
                    word[i].GetLetter();
                else
                    i++;//пропускаем эту букву. Она уже удаляется в GetLetter
            }
            vertical = horizontal = false;
            word.Remove(letter);
        }
        else
        {
            if (word.IndexOf(letter) == 0 || word.IndexOf(letter) == word.Count - 1)
                word.Remove(letter);
            else if (word.IndexOf(letter) > word.IndexOf(coreLetter))
            {
                while (word.Count != word.IndexOf(letter) + 1)
                {
                    if (!word[word.IndexOf(letter) + 1].attached)
                        word[word.IndexOf(letter) + 1].GetLetter();
                    else
                        word.Remove(word[word.IndexOf(letter) + 1]);
                }
                word.Remove(letter);
            }
            else if (word.IndexOf(letter) < word.IndexOf(coreLetter))
            {
                while (letter != word[0])
                {
                    if (!word[0].attached)
                        word[0].GetLetter();
                    else
                        word.Remove(word[0]);
                }
                word.Remove(letter);
            }
        }


        if (word.Count == 1)
        {
            vertical = horizontal = false;
            if (word[0].attached)
                word.RemoveAt(0);
        }

        int counter = 0;
        foreach (Cell_script sc in word)   //если все буквы злова закреплены
        {
            if (sc.attached)
                counter++;
        }
        if (counter == word.Count)
        {
            Debug.Log("clear");
            word.Clear();
            vertical = horizontal = false;
        }


        /*string wordString = "";

        foreach (Cell_script cs in word)
        {
            wordString += cs.attachedLetter.letter;
        }

        Debug.Log(wordString);*/
        gm.ButtonUpdate();
    }

    public void ButtonUpdate()
    {
        if (word.Count == 0)
            endButtonImage.sprite = endButton[0];
        else
            endButtonImage.sprite = endButton[1];

        if(Cursor_script.exchange)
            endButtonImage.color = new Color(0.8f, 0.8f, 0.8f, 1);
        else
            endButtonImage.color = new Color(1, 1, 1, 1);
    }
    public void EndTurn()
    {
        if (word.Count > 0)    //подсчет очков и утверждение позиции словва
        {
            string wordString = "";
            foreach (Cell_script cs in word)
                wordString += cs.attachedLetter.letter;

            if ((dictionaryCheck && FindWord(wordString)) || !dictionaryCheck)
            {
                int count = 0;

                foreach (CellPlate_script cs in players[turn].plate.cells)
                    if (cs.let != null)
                        count++;

                foreach (Cell_script cs in word)
                {
                    cs.selected = false;
                    cs.attached = true;
                    cs.SetPoints();
                }

                if (count == 0 && !endingTurn)
                    players[turn].AddPoints(15);
                players[turn].ChangeLetters();
                Pointmanager_script.MovePoints();

                word.Clear();
                vertical = horizontal = false;
            }
            else
                UI_script.singleton.DoesntExist();
        }
        else    //замена букв
        {
            Cursor_script.exchange = !Cursor_script.exchange;
            ButtonUpdate();
            if (!Cursor_script.exchange)
            {
                int count = 0;
                for (int i = 0; i < 7; i++)
                {
                    if (!players[turn].plate.cells[i].exchanged)
                        count++;
                }
                if (count == 7)
                    return;
                endingTurn = true;
                Cursor_script.ignoreInput = true;
                players[turn].ChangeLetters();
                vertical = horizontal = false;
            }
        }
    }
    public static void Next()
    {
        endingTurn = false;
        gm.ButtonUpdate();
        ShowNumber_script.doubleWord = false;
        ShowNumber_script.tripleWord = false;
        players[turn].TurnEnd();
        turn = (turn + 1) % players.Count;
        players[turn].TurnStart();
        players[turn].ChangeLetters();
        UI_script.NextTurn();
    }
    public bool FindWord(string w)
    {
        TextAsset textFile = dictionary[alphabet.IndexOf(w[0])];
        string[] wordsInText = textFile.text.Split('\n');
        foreach (string currentWord in wordsInText)
        {
            if (currentWord.Contains(w))
            {
                return true;
            }
        }
        return false;
    }
}