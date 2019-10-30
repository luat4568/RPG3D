using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CameraState currentState;
    public CameraState[] cameraStates;
    private Dictionary<CameraStateType, CameraState> dic = new Dictionary<CameraStateType, CameraState>();
    public Transform target;
    [System.NonSerialized]
    public Transform trans;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        trans = transform;
        foreach (CameraState e in cameraStates)
        {
            dic.Add(e.stateType, e);
        }
        ChangeState(CameraStateType.NORMAL);
    }
    public void ChangeState(CameraStateType newState)
    {
        currentState = dic[newState];
    }
    private void LateUpdate()
    {
        trans.position = Vector3.Lerp(trans.position, target.position + currentState.offset, Time.deltaTime*currentState.speedFlow);

        Quaternion q = Quaternion.Euler(currentState.angle);
        trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 6);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, currentState.fov, Time.deltaTime * 6);
    }

    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeState(CameraStateType.SKILL);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeState(CameraStateType.NORMAL);
        }
    }
}
