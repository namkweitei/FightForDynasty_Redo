using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).transform.rotation = transform.parent.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
