using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlate_script : BaseUI_script
{
    public List<Image> letters = new List<Image>();
    public Sprite[] sprites;
    public Camera cam;
    public float speed = 15;

    [SerializeField]
    private Player_script player;
    private char[] playerName = new char[3] { 'à', 'à', 'à' };
    private string alphabet = "àáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ1234567890";
    private int numberOfLetter, index = -1;
    private Image changedLetter;
    private Vector3 letterPos;
    private bool move;
    void Start()
    {

    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
            move = true;
        else if (Input.GetMouseButtonUp(0))
        {
            index = -1;
            move = false;
            changedLetter = null;
            player.playerName = new string(playerName);
        }
        else if (move && index != -1)
        {
            Vector3 deltaPos = letterPos - cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(letterPos);
            if (deltaPos.y > 1 * speed)
            {
                numberOfLetter -= 1;
                numberOfLetter = numberOfLetter < 0 ? 41 : numberOfLetter;
                letterPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (deltaPos.y < -1 * speed)
            {
                numberOfLetter = (numberOfLetter + 1) % 42;
                letterPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            letters[index].sprite = sprites[numberOfLetter];
            playerName[index] = alphabet[numberOfLetter];
        }*/

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && index != -1)
            {
                Vector3 deltaPos = letterPos - cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                if (deltaPos.y > 1 * speed)
                {
                    numberOfLetter -= 1;
                    numberOfLetter = numberOfLetter < 0 ? 41 : numberOfLetter;
                    letterPos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                }
                else if (deltaPos.y < -1 * speed)
                {
                    numberOfLetter = (numberOfLetter + 1) % 42;
                    letterPos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
                }

                letters[index].sprite = sprites[numberOfLetter];
                playerName[index] = alphabet[numberOfLetter];
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                index = -1;
                move = false;
                changedLetter = null;
                player.playerName = new string(playerName);
            }
        }
    }

    public void ChangeLetter(int i)
    {
        index = i;
        index = letters.IndexOf(letters[index]);
        numberOfLetter = alphabet.IndexOf(playerName[index]);
        letterPos = cam.ScreenToWorldPoint(letters[index].rectTransform.position);
    }

    RaycastHit2D Ray()
    {
        Vector3 start = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(start, new Vector3(0, 0, 10));
        return hit;
    }

    private void OnEnable()
    {
        player.gameObject.SetActive(true);
    }
}
