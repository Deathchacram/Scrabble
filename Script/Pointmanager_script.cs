using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointmanager_script : MonoBehaviour
{
    public GameObject point;
    public static List<Point_script> points = new List<Point_script>();
    public static List<Point_script> activePoints = new List<Point_script>();
    public static Pointmanager_script singleton;

    private static int l = 0;
    void Awake()
    {
        singleton = this;
        for(int i = 0; i < 100; i++)
        {
            Instantiate(point, transform.position, Quaternion.identity, transform);
        }
    }

    void Update()
    {

    }

    public static void GetPoint(Vector3 cellPos)
    {
        //int randomInt = Random.Range(0, points.Count);
        activePoints.Add(points[l]);
        points[l].transform.position = cellPos;
        if (GameManager_script.vertical)
        {
            float randomX = (float)(Random.Range(0, 2) * 2 - 1 - Random.Range(-0.3f, 0.3f)) / 1.5f;
            float randomY = Random.Range(0, 0.33f) * 2 - 0.33f;
            points[l].Move(cellPos + new Vector3(randomX, randomY, 0), cellPos, 3, false);
        }
        else
        {
            float randomX = Random.Range(0, 0.33f) * 2 - 0.33f;
            float randomY = (float)(Random.Range(0, 2) * 2 - 1 - Random.Range(-0.3f, 0.3f)) / 1.5f;
            points[l].Move(cellPos + new Vector3(randomX, randomY, 0), cellPos, 3, false);
        }
        l++;
    }
    static public void MovePoints()
    {
        singleton.StartCoroutine(IMovePoints());
        l = 0;
    }

    static IEnumerator IMovePoints()
    {
        Cursor_script.ignoreInput = true;
        yield return new WaitForSeconds(0.4f);
        foreach (Point_script point in activePoints)
        {
            Vector3 end = Score_script.scorePlates[GameManager_script.turn].pointsPos;
            point.Move(end, point.transform.position, 2, true);
            yield return new WaitForSeconds(0.08f);
        }
        yield return new WaitForSeconds(1);
        activePoints.Clear();
        GameManager_script.Next();
    }
}
