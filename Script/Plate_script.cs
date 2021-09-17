using System.Collections;
using UnityEngine;

public class Plate_script : MonoBehaviour
{
    public GameObject[] cellsPlates;
    [HideInInspector]
    public CellPlate_script[] cells = new CellPlate_script[7];
    void Awake()
    {
        for (int i = 0; i < 7; i++)
        {
            cells[i] = cellsPlates[i].GetComponent<CellPlate_script>();
            cells[i].parentPlate = this;
        }
        for (int i = 0; i < 7; i++)
        {
            cells[i].neighbours = cells;
        }
    }

    public void SetLetters()
    {
        StartCoroutine(TestCoroutine());
    }
    public void ChangeLetters()
    {
        foreach (CellPlate_script cs in cells)
        {
            if (cs.exchanged)
            {
                cs.Exchange();
                Letter_script letter = cs.let;
                letter.Translate(new Vector3(20, 0, 0));
                letter.parent = null;
                cs.let = null;
                SetupLetters_script.lettersInBank.Add(letter);
            }
        }
        Invoke("SetLetters", 0.05f);
    }
    public IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 7; i++)
        {
            if (cells[i].let == null)
            {
                cells[i].GetRandomLetter();
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.5f);
        if (GameManager_script.endingTurn)
            GameManager_script.Next();
    }
}
