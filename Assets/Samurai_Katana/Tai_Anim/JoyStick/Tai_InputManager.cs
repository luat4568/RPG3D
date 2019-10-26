using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tai_InputManager : MonoBehaviour
{
    [SerializeField]
    private Tai_JoyStickInput moveJoystick;
    public static Vector2 moveDir;
   
    public static bool isMove = true;
    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") + moveJoystick.moveDir.x;
        float y = Input.GetAxis("Vertical") + moveJoystick.moveDir.y;
        moveDir = new Vector2(x, y);
        if (!isMove)
        {
            moveDir = Vector2.zero;
        }
        
    }
}
