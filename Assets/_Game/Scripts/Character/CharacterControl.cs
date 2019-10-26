using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public CharacterDataBiding dataBiding;
    private Transform trans;
    public float speedRotate=5;
    public float speedMove = 5;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = InputManager.moveDir;
        if(moveDir.magnitude>0)
        {
            Quaternion q = Quaternion.LookRotation(moveDir, Vector3.up);

            Quaternion qc = trans.localRotation;

            qc = Quaternion.Slerp(qc, q, Time.deltaTime * speedRotate);

            trans.localRotation = qc;
           // trans.Translate(Vector3.forward * moveDir.magnitude * Time.deltaTime * speedMove);
        
        }

        dataBiding.SpeedMove = moveDir.magnitude;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Debug.LogError("ik");
    }

    //private void OnAnimatorMove()
    //{

    //}

}
