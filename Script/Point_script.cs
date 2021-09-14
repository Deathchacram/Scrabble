using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_script : MonoBehaviour
{

    private static Point_script singleton;
    private bool isMove, addPoints;
    private float speed = 2, stage = 0;
    private Vector3 endPoint, startPoint;

    void Start()
    {
        Pointmanager_script.points.Add(this);
    }

    void Update()
    {
        if (isMove)
        {
            stage += Time.deltaTime * speed;
            Vector3 pos = Vector3.Lerp(startPoint, endPoint, stage);
            transform.position = pos;
            if (stage > 1)
            {
                stage = 0;
                isMove = false;
                if (addPoints)
                {
                    GameManager_script.players[GameManager_script.turn].AddPoints(1);
                    transform.position = Pointmanager_script.singleton.transform.position;
                    addPoints = false;
                }
            }
        }
    }

    public void Move(Vector3 EndPoint, Vector3 StartPoint, float Speed, bool a)
    {
        speed = Speed;
        endPoint = EndPoint;
        startPoint = StartPoint;
        isMove = true;
        addPoints = a;
    }
}
