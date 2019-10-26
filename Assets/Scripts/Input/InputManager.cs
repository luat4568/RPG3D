using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private JoyStickInput moveJoystick;
    public static Vector3 moveDir = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float _x, _y;
    // Update is called once per frame
    void Update()
    {
        _x = Input.GetAxis("Horizontal") + moveJoystick.moveDir.x;
        _y = Input.GetAxis("Vertical") + moveJoystick.moveDir.y;
        moveDir.x = _x;
        moveDir.z = _y;
    }
}
