using UnityEngine;

public class Player_script : MonoBehaviour
{
    public Vector3 platePos;
    public GameObject platePrefab;
    public string playerName;
    public bool isHuman;
    [HideInInspector]
    public Plate_script plate;

    private Score_script scorePlate;
    private int score = 0;

    void Start()
    {
        gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        GameManager_script.players.Add(this);
        GameObject obj = Instantiate(platePrefab, platePos, Quaternion.identity);
        plate = obj.GetComponent<Plate_script>();
        plate.SetLetters();
        for (int i = 0; i < 4; i++)
        {
            if (!Score_script.scorePlates[i].hasParent)
            {
                scorePlate = Score_script.scorePlates[i];
                i = 4;
            }
        }
        scorePlate.playerName = playerName;
        scorePlate.hasParent = true;
        scorePlate.isHuman = isHuman;
        scorePlate.UpdateScore(score);
    }
    public void EndGame()
    {
        plate = null;
        scorePlate = null;
    }

    public void AddPoints(int points)
    {
        score += points;
        scorePlate.UpdateScore(score);
    }
    public void ChangeLetters()
    {
        plate.ChangeLetters();
    }
    public void TurnEnd()
    {
        foreach (CellPlate_script cs in plate.cells)
        {
            if (cs.let != null)
                cs.let.Hide();
        }
        plate.gameObject.SetActive(false);
    }
    public void TurnStart()
    {
        plate.gameObject.SetActive(true);
        foreach (CellPlate_script cs in plate.cells)
        {
            if (cs.let != null)
                cs.let.Show();
        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
