using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNumber_script : MonoBehaviour
{
    public bool isScaleUp = true, isScaleDown, isMultipied;
    public static bool doubleWord, tripleWord;
    public int score = 1, multiplier = 1;
    public static List<ShowNumber_script> showNums = new List<ShowNumber_script>();

    [SerializeField]
    private Sprite[] numbers;[SerializeField]
    private Sprite[] multiplierSprites;
    private string nums = "12345678910", mult = "23469";
    private float speed = 6, stage;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        int n = nums.IndexOf(score.ToString());
        sr.sprite = numbers[n];

        if (doubleWord)
            multiplier *= 2;
        if (tripleWord)
            multiplier *= 3;

        for (int i = 0; i < score * multiplier; i++)
        {
            Pointmanager_script.GetPoint(transform.position);
        }
    }
    private void Update()
    {
        if (isScaleUp)
        {
            stage += Time.deltaTime * speed;
            float scale = Mathf.Lerp(0, 1, stage);
            transform.localScale = new Vector3(scale, scale, 0);
            if (stage > 1)
            {
                stage = 0;
                isScaleUp = false;
                StartCoroutine(Wait());
            }
        }
        if (isScaleDown)
        {
            stage += Time.deltaTime * speed;
            float scale = Mathf.Lerp(1, 0, stage);
            transform.localScale = new Vector3(scale, scale, 0);
            if (stage > 1)
            {
                stage = 0;
                isScaleDown = false;
                if (!isMultipied)
                {

                    if (multiplier != 1)
                    {
                        isScaleUp = isMultipied = true;
                        sr.sprite = multiplierSprites[mult.IndexOf(multiplier.ToString())];
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
                if (!isMultipied)
                    Destroy(gameObject);
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        isScaleDown = true;
    }
}
