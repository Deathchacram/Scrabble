using System;
using UnityEngine;
using UnityEngine.UI;

public class Cursor_script : MonoBehaviour
{
    public Canvas canvas;
    public Text text, text2;
    public static bool exchange, ignoreInput;
    public static Camera cam;

    private Letter_script collectedObj;
    private CellPlate_script tappedCellPlate;
    private bool down;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !ignoreInput && !exchange)
        {
            down = true;
            RaycastHit2D hit = Ray();
            Vector3 touchPos = (Input.mousePosition);
            if (!exchange)
            {
                if (hit)
                    if (hit.transform.tag == "Letter")    //подобрать букву
                    {
                        tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                        if (tappedCellPlate.let != null)
                        {
                            collectedObj = tappedCellPlate.let;
                            collectedObj.parent = null;
                            tappedCellPlate.let = null;
                            Cell_script.ShowAvailable();
                        }
                    }
            }
        }
        else if (Input.GetMouseButtonUp(0) && !ignoreInput && !exchange)
        {
            down = false;
            RaycastHit2D hit = Ray();
            Vector3 touchPos = (Input.mousePosition);
            Cell_script.StopShowAvailable();
            if (collectedObj != null && hit)
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Field cell")   //установка буквы на поле
                {
                    Cell_script cell = hit.transform.GetComponent<Cell_script>();
                    if (cell.freedom)
                    {
                        cell.attachedLetter = collectedObj;
                        cell.SetLetter();
                        collectedObj = null;
                    }
                    else if (cell.selected)
                    {
                        cell.ChangeLetter(collectedObj);
                        collectedObj = null;
                    }
                    else
                    {
                        collectedObj.FindCellPlate();
                        collectedObj.Translate();
                        collectedObj = null;
                    }
                }
                else   //отправить на каретку
                {
                    collectedObj.FindCellPlate();
                    collectedObj.Translate();
                    collectedObj = null;
                }
            }
            else if (collectedObj == null && hit)  //освободить букву на поле
            {
                if (hit.transform.tag == "Field cell")
                {
                    Cell_script cell = hit.transform.GetComponent<Cell_script>();
                    if (cell.selected)
                    {
                        cell.GetLetter();
                    }
                }
            }
            else
            {
                try
                {
                    collectedObj.FindCellPlate();
                    collectedObj.Translate();
                    collectedObj = null;
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }
            tappedCellPlate = null;
        }
        else if (Input.GetMouseButtonDown(0) && exchange)
        {
            RaycastHit2D hit = Ray();
            Vector3 touchPos = (Input.mousePosition);
            if (hit.transform.tag == "Letter")    //подобрать букву
            {
                tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                if (tappedCellPlate.let != null)
                {
                    tappedCellPlate.Exchange();
                    Debug.Log("1111");
                }
            }
        }
        else if (down && !ignoreInput)
        {
            RaycastHit2D hit = Ray();
            Vector3 touchPos = (Input.mousePosition);
            if (collectedObj != null)
            {
                touchPos = cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 0));
                collectedObj.transform.position = new Vector3(touchPos.x, touchPos.y, 0);
            }
            if (hit)
            {
                if (hit.transform.tag == "Letter" && tappedCellPlate == null && collectedObj != null)
                {
                //    text.text = " Upped";
                //    tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                //    tappedCellPlate.Up();
                }
                else if (hit.transform.tag == "Letter" && tappedCellPlate != null && collectedObj != null)
                {
                    /*CellPlate_script cps = hit.transform.GetComponent<CellPlate_script>();
                    if (cps != tappedCellPlate)
                    {
                        tappedCellPlate.Down();
                        tappedCellPlate = cps;
                        tappedCellPlate.Up();
                    }*/
                }
            }
            else if ((!hit || hit.transform.tag != "Letter") && tappedCellPlate != null && collectedObj != null)
            {
                text.text = "start downed";
                tappedCellPlate.Down();
                text.text += " downed";
                tappedCellPlate = null;
            }
        }

        /*if (Input.touchCount > 0 && !ignoreInput)
        {
            RaycastHit2D hit = Ray();

            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (!exchange)
            {
                if (collectedObj != null)
                {
                    touchPos = cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 0));
                    collectedObj.transform.position = new Vector3(touchPos.x, touchPos.y, 0);
                }
                if (touch.phase == TouchPhase.Began)                                            //TouchPhase Began
                {
                    if (hit.transform.tag == "Letter")    //подобрать букву
                    {
                        tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                        if (tappedCellPlate.let != null)
                        {
                            collectedObj = tappedCellPlate.let;
                            collectedObj.parent = null;
                            tappedCellPlate.let = null;
                            Cell_script.ShowAvailable();
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)   //TouchPhase Move
                {
                    hit = Ray();
                    if (hit)
                    {
                        if (hit.transform.tag == "Letter" && tappedCellPlate == null && collectedObj != null)
                        {
                            text.text = " Upped";
                            tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                            tappedCellPlate.Up();
                        }
                        else if (hit.transform.tag == "Letter" && tappedCellPlate != null && collectedObj != null)
                        {
                            CellPlate_script cps = hit.transform.GetComponent<CellPlate_script>();
                            if (cps != tappedCellPlate)
                            {
                                tappedCellPlate.Down();
                                tappedCellPlate = cps;
                                tappedCellPlate.Up();
                            }
                        }
                    }
                    else if ((!hit || hit.transform.tag != "Letter") && tappedCellPlate != null && collectedObj != null)
                    {
                        text.text = "start downed";
                        tappedCellPlate.Down();
                        text.text += " downed";
                        tappedCellPlate = null;
                    }
                }
                else if (touch.phase == TouchPhase.Ended)                                                 //TouchPhase End
                {
                    Cell_script.StopShowAvailable();
                    if (collectedObj != null && hit)
                    {
                        if (hit.transform.tag == "Field cell")   //установка буквы на поле
                        {
                            Cell_script cell = hit.transform.GetComponent<Cell_script>();
                            if (cell.freedom)
                            {
                                cell.attachedLetter = collectedObj;
                                cell.SetLetter();
                                collectedObj = null;
                            }
                            else if (cell.selected)
                            {
                                cell.ChangeLetter(collectedObj);
                                collectedObj = null;
                            }
                            else
                            {
                                collectedObj.FindCellPlate();
                                collectedObj.Translate();
                                collectedObj = null;
                            }
                        }
                        else   //отправить на каретку
                        {
                            collectedObj.FindCellPlate();
                            collectedObj.Translate();
                            collectedObj = null;
                        }
                    }
                    else if (collectedObj == null && hit)  //освободить букву на поле
                    {
                        if (hit.transform.tag == "Field cell")
                        {
                            Cell_script cell = hit.transform.GetComponent<Cell_script>();
                            if (cell.selected)
                            {
                                cell.GetLetter();
                            }
                        }
                    }
                    else
                    {
                        collectedObj.FindCellPlate();
                        collectedObj.Translate();
                        collectedObj = null;
                    }
                    tappedCellPlate = null;
                }
            }
            else
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (hit.transform.tag == "Letter")    //подобрать букву
                    {
                        tappedCellPlate = hit.transform.GetComponent<CellPlate_script>();
                        if (tappedCellPlate.let != null)
                        {
                            tappedCellPlate.Exchange();
                        }
                    }
                }
            }
        }*/
    }
    RaycastHit2D Ray()
    {
        Vector3 start = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(start, new Vector3(0, 0, 10));
        return hit;
    }
}
