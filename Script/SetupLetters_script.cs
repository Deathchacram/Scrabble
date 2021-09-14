using System.Collections.Generic;
using UnityEngine;

public class SetupLetters_script : MonoBehaviour
{
    public static List<Letter_script> lettersInBank = new List<Letter_script>();
    public GameObject letterPrefab;
    public Sprite[] sprites;

    private int[] point = new int[32] { 1, 3, 2, 3, 2, 1, 5, 5, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 3, 10, 5, 10, 5, 10, 10, 10, 5, 5, 10, 10, 3 };  //очки за буквы от а до я (без букввы ё)
    private int[] countOfLetter = new int[32] { 10, 3, 5, 3, 5, 9, 2, 2, 8, 3, 6, 4, 5, 8, 10, 6, 6, 6, 5, 3, 1, 2, 1, 2, 1, 1, 1, 2, 2, 1, 1, 3 };  //количество букв
    private string alphabet = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

    void Awake()
    {
        for (int i = 0; i < 32; i++)
        {
            for (int l = 0; l < countOfLetter[i]; l++)
            {
                GameObject let = Instantiate(letterPrefab, transform.position, Quaternion.identity, transform);
                Letter_script ls = let.GetComponent<Letter_script>();
                ls.sprite = sprites[i];
                ls.letter = alphabet[i];
                ls.points = point[i];
                lettersInBank.Add(ls);
            }
        }
    }
}
