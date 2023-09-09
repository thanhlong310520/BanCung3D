using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRecceiver : MyMonoBehaviour
{

    [SerializeField] protected int hp = 1;
    [SerializeField] protected int hpMax = 3;
    [SerializeField] protected bool isDead = false;


    protected override void Start()
    {
        base.Start();
        this.Reborn();
    }
    public virtual void Reborn()
    {
        hp = hpMax;
        isDead = false;
    }
    public virtual void Add(int add)
    {
        hp += add;
        if (hp > hpMax) hp = hpMax;
    }
    public virtual void Deduct(int deduct,Transform val)
    {
        hp -= deduct;
        if (hp <= 0) hp = 0;
        this.CheckIsDead();
    }

    protected virtual void CheckIsDead()
    {
        if (!IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected virtual bool IsDead()
    {
        return this.hp <= 0;
    }
    protected virtual void OnDead()
    {
        //override
    }
    public bool GetIsDead()
    {
        return isDead;
    }
    public int GetHpMax()
    {
        return hpMax;
    }
    public int GetHp()
    {
        return hp;
    }
}
