using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    public Transform target;
    [System.NonSerialized]
    public Transform trans;
    public bool isFlowTarget;
    // Start is called before the first frame update
    void Start()
    {
        isFlowTarget = true;
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlowTarget)
            trans.position = target.position;
    }
}
