using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tai_CharacterControl : MonoBehaviour
{
    public Tai_CharacterBinding dataBinding;
    private Vector3 tempMoveDir;
    public Transform trans;
    public float speed = 1f;
    private Vector3 move;
    public LayerMask mask;
    public Transform trans_Anchor;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        //WeaponControl.OnWeaponChange += OnWeaponChange;
    }

    //void OnWeaponChange(WeaponBehaviour obj)
    //{
    //    obj.aimShoot = trans_Anchor;
    //}


    // Update is called once per frame
    void Update()
    {
        float x = Tai_InputManager.moveDir.x;
        float y = Tai_InputManager.moveDir.y;
        // x = Mathf.Round(x);
        //y = Mathf.Round(y);
        Vector3 posMove = new Vector3(x, y, trans.position.z);
        move = new Vector3(x, y);
        Debug.Log(posMove);
        float speedMove = move.magnitude;
        speedMove = Mathf.RoundToInt(speedMove);
        if (speedMove > 0)
        {

            Vector3 newMoveDir = new Vector3(x, y);
            if (tempMoveDir != newMoveDir)
            {

                tempMoveDir = newMoveDir;
            }
            dataBinding.MoveDir = tempMoveDir;
            trans_Anchor.up = posMove.normalized;
        }

        RaycastHit2D raycast = Physics2D.Raycast(trans.position + posMove.normalized * 0.1f, posMove, 0.5f, mask);
        if (raycast.collider != null)
        {
            if (raycast.collider.tag == "Down")
            {
                speedMove = 0;
            }
            else if (raycast.collider.tag == "Up")
            {
                // raycast.collider.GetComponentInParent<SpriteRenderer>().sortingOrder = 3;

            }
            else
            {

            }

        }

        dataBinding.SpeedMove = speedMove;
        trans.position = Vector3.MoveTowards(trans.position, trans.position + posMove,
         speedMove * speed * Time.deltaTime);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(trans.position, trans.position + move * 2f);
    }
}
