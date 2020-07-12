using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedFollow : MonoBehaviour
{
    public Transform limitedPosition1;
    public Transform limitedPosition2;
    public Transform targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = limitedPosition1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
