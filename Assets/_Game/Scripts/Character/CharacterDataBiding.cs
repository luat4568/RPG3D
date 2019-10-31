using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationData
{
    public float timeAttack;
    public float timeAnim;
    public int index;
}
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
    public int IndexCombo
    {
        set
        {
            animator.SetInteger(key_IndexCombo, value);
        }
    }
    public bool Attack
    {
        set
        {
            if (value)
            {
                animator.SetTrigger(key_Attack);
            }
        }
    }
    private int key_SpeedMove;
    private int key_IndexCombo;
    private int key_Attack;
    // Start is called before the first frame update
    void Start()
    {
        key_SpeedMove = Animator.StringToHash("SpeedMove");
        key_IndexCombo = Animator.StringToHash("indexCombo");
        key_Attack = Animator.StringToHash("Attack");
    }

}
