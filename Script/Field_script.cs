using UnityEngine;

public class Field_script : MonoBehaviour
{
    public int size;
    public GameObject cell;

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            for (int l = 0; l < size; l++)
            {
                Instantiate(cell, new Vector3(i * 0.48f - 2.88f, l * 0.48f - 2.88f), Quaternion.identity, transform);
            }
        }
        /*for (int i = 0; i < size * size - 1; i++)
        {
            Cell_script.cells[i].SetNeighbours();
        }*/
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Cell_script.cells.Length);
            for (int i = 0; i < size * size; i++)
            {
                Cell_script.cells[i].SetNeighbours();
            }
        }
    }
}
