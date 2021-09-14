using UnityEngine;

public class CellPlate_script : MonoBehaviour
{

    public CellPlate_script[] neighbours = new CellPlate_script[7];
    public Letter_script let;
    public Sprite newLetter;
    public Plate_script parentPlate;
    [HideInInspector]
    public bool exchanged;

    [SerializeField]
    private int number;

    void Awake()
    {
        
    }

    public void Up()
    {
        if (number + 1 <= 6)
        {
            neighbours[number + 1].GetUp(let);
            let = null;
        }
    }

    public void Down()
    {
        if (number + 1 <= 6)
        {
            neighbours[number + 1].GetDown(this);
            if (let != null)
                let.Translate();
        }
    }

    private void GetUp(Letter_script ls)
    {
        if (number + 1 <= 6)
        {
            neighbours[number + 1].GetUp(let);
            let = ls;
            if (let != null)
            {
                let.parent = this;
                let.Translate();
            }
        }
        else
        {
            let = ls;
            if (let != null)
            {
                let.parent = this;
                let.Translate();
            }
        }
    }
    private void GetDown(CellPlate_script cp)
    {
        cp.let = let;
        if (let != null)
        {
            cp.let.parent = cp;
            cp.let.Translate();
        }
        let = null;
        if (number + 1 <= 6)
        {
            neighbours[number + 1].GetDown(this);
        }
    }
    public void GetRandomLetter()
    {
        if (SetupLetters_script.lettersInBank.Count > 0)
        {
            int randomNumber = Random.Range(0, SetupLetters_script.lettersInBank.Count);
            Letter_script randomLetter = SetupLetters_script.lettersInBank[randomNumber];
            let = randomLetter;
            randomLetter.parent = this;
            randomLetter.Translate();
            SetupLetters_script.lettersInBank.RemoveAt(randomNumber);
        }
    }
    public void Exchange()
    {
        exchanged = !exchanged;
        if (exchanged)
        {
            let.sr.sprite = newLetter;
        }
        else
            let.sr.sprite = let.sprite;
    }
}
