using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter_script : MonoBehaviour
{
    public float speed = 8;
    public CellPlate_script parent;
    public char letter;
    public int points = 0;  //очки за букву
    public Sprite sprite;
    [HideInInspector]
    public SpriteRenderer sr;

    private bool isTranslated;
    private Vector3 endPoint, start, oldEndPoint;
    private float stage = 0;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }

    void Update()
    {
        if(isTranslated)
        {
            stage += Time.deltaTime * speed;
            Vector3 pos = Vector3.Lerp(start, endPoint, stage);
            transform.position = pos;
            if(stage > 1)
            {
                stage = 0;
                isTranslated = false;
            }
        }
        else
        {
            stage = 0;
            isTranslated = false;
        }
    }
    public void Translate()
    {
        if (stage != 0)
        {
            stage = 1 - stage;
            isTranslated = true;
            endPoint = parent.transform.position;
            start = oldEndPoint;
            oldEndPoint = endPoint;
        }
        else
        {
            isTranslated = true;
            endPoint = parent.transform.position;
            oldEndPoint = endPoint;
            start = transform.position;

        }
    }
    public void Translate(Vector3 t)
    {
        if (stage != 0)
        {
            stage = 1 - stage;
            isTranslated = true;
            endPoint = t;
            start = oldEndPoint;
            oldEndPoint = endPoint;
        }
        else
        {
            isTranslated = true;
            endPoint = t;
            oldEndPoint = endPoint;
            start = transform.position;

        }
    }
    public void FindCellPlate()
    {
        if (parent == null)
        {
            int number = 0;
            while (parent == null)
            {
                CellPlate_script c = GameManager_script.players[GameManager_script.turn].plate.cells[number];  //потом исправить этот кошмар 
                if (c != null)
                    if (c.let == null)
                    {
                        parent = c;
                        c.let = this;
                    }
                number++;
            }
        }
    }
    public void Hide()
    {
        sr.sprite = null;
    }
    public void Show()
    {
        sr.sprite = sprite;
    }
}
