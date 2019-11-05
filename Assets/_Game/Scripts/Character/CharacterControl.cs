using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackData
{
    public int damage;
    public float force;
    public Transform trans;
}
public class CharacterControl : MonoBehaviour
{
    // Public
    public CharacterDataBiding dataBiding;
    public List<AnimationData> animCombo;
    public float rangeDetect;
    public float speedRotate=5;

    // Private
    private Transform trans;
    private Dictionary<int, AnimationData> dicAnimCombo = new Dictionary<int, AnimationData>();
    private AnimationData currentAnimationData;
    private float timeCount = 0;
    private float rof = 0;
    private int indexCombo = 0;
    private EnemyControl currentEnemy;
    private bool isFire_;
    private bool IsFire
    {
        set
        {
            if (value)
            {
                dataBiding.Attack = true;
                indexCombo++;
                currentAnimationData = dicAnimCombo[indexCombo];
                rof = currentAnimationData.timeAnim;
                timeCount = 0;
                //Check Target
                currentEnemy = GetTarget();
                if (currentEnemy != null)
                {
                    //thuc hien dash
                    DashToTarget();
                }
            }
            else
            {
                if (currentEnemy != null)
                {
                    
                }
                else
                {
                    indexCombo = 0;
                }
                if(indexCombo >= 4)
                {
                    indexCombo = 0;
                }
            }
            dataBiding.IndexCombo = indexCombo;
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
        else
        {
            // Di chuyen nhan vat
            Vector3 moveDir = InputManager.moveDir;
            if (moveDir.magnitude > 0)
            {
                Quaternion q = Quaternion.LookRotation(moveDir, Vector3.up);
                Quaternion qc = trans.localRotation;
                qc = Quaternion.Slerp(qc, q, Time.deltaTime * speedRotate);
                trans.localRotation = qc;
                // trans.Translate(Vector3.forward * moveDir.magnitude * Time.deltaTime * speedMove);
            }
            dataBiding.SpeedMove = moveDir.magnitude;
        }
        if(timeCount >= rof)
        {
            IsFire = false;
            currentEnemy = null;
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
    public EnemyControl GetTarget()
    {
        currentEnemy = null;
        EnemyControl enemy = null;
        int enemyMask = 1 << 9;
        Collider[] hitColliders = Physics.OverlapSphere(trans.position, rangeDetect, enemyMask);
        List<EnemyTargetSelect> lstarget = new List<EnemyTargetSelect>();
        foreach (Collider e in hitColliders)
        {
            Vector3 dir = e.transform.position - trans.position;
            float dot = Vector3.Dot(trans.forward, dir.normalized);
            if(dot >= 0)
            {
                EnemyControl enemy_ = e.GetComponent<EnemyControl>();
                float dis = Vector3.Distance(e.transform.position, trans.position);
                lstarget.Add(new EnemyTargetSelect { enemyControl = enemy_, distance = dis, angle = dot });
            }
        }
        lstarget.Sort();
        if(lstarget.Count > 0)
        {
            enemy = lstarget[0].enemyControl;
        }
        return enemy;
    }
    public List<EnemyTargetSelect> GetTarget(float dotLimit)
    {
        int enemyMask = 1 << 9;
        Collider[] hitColliders = Physics.OverlapSphere(trans.position, rangeDetect, enemyMask);
        List<EnemyTargetSelect> lstarget = new List<EnemyTargetSelect>();
        foreach (Collider e in hitColliders)
        {
            Vector3 dir = e.transform.position - trans.position;
            float dot = Vector3.Dot(trans.forward, dir.normalized);
            if (dot >= dotLimit)
            {
                EnemyControl enemy_ = e.GetComponent<EnemyControl>();
                float dis = Vector3.Distance(e.transform.position, trans.position);
                lstarget.Add(new EnemyTargetSelect { enemyControl = enemy_, distance = dis, angle = dot });
            }
        }
        lstarget.Sort();
        return lstarget;
    }
    private void DashToTarget()
    {
        if(currentEnemy != null)
        {
            Vector3 dir = currentEnemy.transform.position - trans.position;
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.localRotation = q;
            float dis = Vector3.Distance(currentEnemy.transform.position, trans.position);
            if(dis > currentAnimationData.dashLimit)
            {
                Vector3 posAim = currentEnemy.transform.position - dir.normalized * currentAnimationData.dashLimit;
                trans.DOMove(posAim, currentAnimationData.timeAttack).OnComplete(ApplyDamage);
            }
        }
    }
    private void ApplyDamage()
    {
        List<EnemyTargetSelect> ls = GetTarget(currentAnimationData.angleForce);
        CharacterAttackData attackData = new CharacterAttackData
        {
            damage = 0,
            force = currentAnimationData.force,
            trans = this.trans
        };
        foreach (EnemyTargetSelect e in ls)
        {
            e.enemyControl.ApplyDamage(attackData);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (trans != null)
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireArc(trans.position, trans.up, -trans.right, 180, rangeDetect);
        }
    }
#endif
}

//------------------
public class EnemyTargetSelect : System.IComparable<EnemyTargetSelect>
{
    public EnemyControl enemyControl;
    // lay be hon
    public float distance;
    // lay lon hon
    public float angle;

    public int CompareTo(EnemyTargetSelect other)
    {
        //so sanh
        if (this.distance < other.distance) return - 1;
        else if (this.distance > other.distance) return 1;
        else
        {
            if (this.angle > other.angle) return 1;
            else if (this.angle > other.angle) return -1;
            else return 0;
        }
    }
}
    
