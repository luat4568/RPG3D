using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBiding : MonoBehaviour
{
    public Animator animator;
    public float SpeedMove
    {
        set
        {
            animator.SetFloat(key_SpeedMove, value);
        }
    }
    private int key_SpeedMove;
    // Start is called before the first frame update
    void Start()
    {
        key_SpeedMove = Animator.StringToHash("SpeedMove");
    }

}
