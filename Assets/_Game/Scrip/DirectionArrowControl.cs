using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrowControl : Singleton<DirectionArrowControl>
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform starPos;
    [SerializeField] private Transform endPos;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, starPos.position);
        lineRenderer.SetPosition(1, endPos.position);
    }
    public void SetPos(Transform endPos, Transform starPos)
    {
        this.endPos = endPos;
        this.starPos = starPos;
    }
}
