using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField]
    private JoyStickInput moveJoystick;
    public static Vector3 moveDir = Vector2.zero;
    private float _x, _y;
    public event Action OnFireHandle;
    void Awake()
    {
        instance = this;    
    }
    // Update is called once per frame
    void Update()
    {
        //Control di chuyen
        _x = Input.GetAxis("Horizontal") + moveJoystick.moveDir.x;
        _y = Input.GetAxis("Vertical") + moveJoystick.moveDir.y;
        moveDir.x = _x;
        moveDir.z = _y;
        //Control Attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFireHandle?.Invoke();
        }
    }
}
