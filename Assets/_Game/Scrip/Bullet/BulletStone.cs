using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletStone : GameUnit
{
    [SerializeField] Transform startPoint;  // Replace with your desired start point
    [SerializeField] Vector3 controlPoint;  // Replace with your desired control point 2
    [SerializeField] Character endPoint;  // Replace with your desired end point
    [SerializeField] float t;
    [SerializeField] float speed;
    [SerializeField] float damage;

    private void Update()
    {
        Debug.Log("2");
        CurveCaculate();
    }

    public void OnInit(Transform s, Character e, float d)
    {
        Debug.Log("1");
        startPoint = s;
        controlPoint = (s.position + e.transform.position) * 0.5f; ;
        endPoint = e;
        damage = d;
        t = 0f;
    }

    void CurveCaculate()
    {
        if (t > 1f)
        {
            endPoint.OnHit(damage);
            SmartPool.Ins.Despawn(gameObject);
        }
        t += (Time.deltaTime / 2f) * speed;
        Vector3 pointOnCurve = BezierCurve.QuadraticBezier(startPoint.position, controlPoint, endPoint.transform.position, t);
        transform.position = pointOnCurve;
    }
}
