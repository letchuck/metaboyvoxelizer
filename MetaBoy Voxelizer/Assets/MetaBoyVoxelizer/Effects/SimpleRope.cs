using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRope : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public LineRenderer Line;
    public Transform MidPoint;

    // Update is called once per frame
    void Update()
    {
        if(StartPoint && EndPoint)
        {
            if (Line.positionCount >= 2)
            {
                Line.SetPosition(0, this.transform.InverseTransformPoint(StartPoint.transform.position));
                Line.SetPosition(Line.positionCount - 1, this.transform.InverseTransformPoint(EndPoint.position));
            }
        }
        else if(MidPoint)
        {
            if (Line.positionCount >= 3)
            {
                Line.SetPosition(1, this.transform.InverseTransformPoint(MidPoint.transform.position));
            }
        }
        
    }
}
