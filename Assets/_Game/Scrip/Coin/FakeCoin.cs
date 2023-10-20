
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FakeCoin : GameUnit
{
    [SerializeField] Transform startPoint;  // Replace with your desired start point
    [SerializeField] Vector3 controlPoint;  // Replace with your desired control point 2
    [SerializeField] Transform endPoint;  // Replace with your desired end point
    [SerializeField] float t;
    [SerializeField] float speed;
    [SerializeField] private Vector3 rotaion;
    [SerializeField] private float sp;

    private void Update()
    {
        transform.Rotate(rotaion * sp * Time.deltaTime);
        CurveCaculate();
    }

    public void OnInit(Transform s, Transform e)
    {
        startPoint = s;
        controlPoint = (s.position + e.transform.position) * 0.5f + Vector3.up * 2.5f;
        endPoint = e;
        t = 0f;
    }

    void CurveCaculate()
    {
        if (t > 1f)
        {
            SmartPool.Ins.Despawn(gameObject);
        }
        t += (Time.deltaTime / 2f) * speed;
        Vector3 pointOnCurve = BezierCurve.QuadraticBezier(startPoint.position + Vector3.up, controlPoint, endPoint.transform.position, t);
        transform.position = pointOnCurve;
    }
}
