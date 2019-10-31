using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public CharacterDataBiding dataBiding;
    private Transform trans;
    public List<AnimationData> animCombo;
    private Dictionary<int, AnimationData> dicAnimCombo = new Dictionary<int, AnimationData>();
    private AnimationData currentAnimationData;
    private float timeCount = 0;
    private float rof = 0;
    public float speedRotate=5;
    private int indexCombo = 0;
    private bool isFire_;
    private bool IsFire
    {
        set
        {
            if (value)
            {
                dataBiding.Attack = true;
                indexCombo++;
                dataBiding.IndexCombo = indexCombo;
                currentAnimationData = dicAnimCombo[indexCombo];
                rof = currentAnimationData.timeAnim;
                timeCount = 0;
            }
            isFire_ = value;
        }
        get
        {
            return isFire_;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        foreach (AnimationData e in animCombo)
        {
            dicAnimCombo.Add(e.index, e);
        }
        InputManager.instance.OnFireHandle += OnFireHandle;
    }
    void OnFireHandle()
    {
        if (IsFire)
            return;
        IsFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyen nhan vat
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
        //-----------------------
        //Attack
        timeCount += Time.deltaTime;
        if (IsFire)
        {
            if(currentAnimationData.timeAttack > 0 && timeCount >= currentAnimationData.timeAttack)
            {
                IsFire = false;
            }
        }
        if(timeCount >= rof)
        {
            indexCombo = 0;
            IsFire = false;
            dataBiding.IndexCombo = indexCombo;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Debug.LogError("ik");
    }

    //private void OnAnimatorMove()
    //{

    //}
    // luon duoc goi
    public void OnEventAnimAttack()
    {
        //IsFire = false;
    }
    // se khong duoc goi khi mot combo moi duoc play
    public void OnEventAnimEnd()
    {
        //indexCombo = 0;
        //IsFire = false;
        //dataBiding.IndexCombo = indexCombo;
    }
}
