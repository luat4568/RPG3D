using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class EnemyControl : MonoBehaviour
{
    public bool isAlive = true;
    public int hp = 5;
    
    public virtual void ApplyDamage(CharacterAttackData data,Action<object> callback = null)
    {
        hp -= data.damage;
        Vector3 dir = transform.position - data.trans.position;
        float dis = Vector3.Distance(transform.position, data.trans.position);
        Vector3 posAim = transform.position + dir.normalized * data.force / dis;
        transform.DOMove(posAim, 0.05f).SetEase(Ease.InFlash);
        if(hp <= 0)
        {
            callback?.Invoke(this);
        }
    }
}
