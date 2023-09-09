using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [SerializeField] PlayerCtrl playerCtrl;

    List<ItemOwner> items = new List<ItemOwner>();
    [SerializeField] ItemData allItem;
    private void OnEnable()
    {
    }
    void Start()
    {
        items = GameManager.Instance.testList;
        allItem = GameCtrl.Instance.ItemData;
        playerCtrl = gameObject.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        UseHealItem();
        UseFixBowItem();
        
    }

    void UseHealItem()
    {
        if (InputManager.instance.IsUseHeal())
        {
            int hpP = playerCtrl.PlayerDamageRecciever.GetHp();
            int maxhpP = playerCtrl.PlayerDamageRecciever.GetHpMax();
            if (hpP < maxhpP)
            {
                SqwanItem(4);
            }
        }
    }
    void UseFixBowItem()
    {
        if (InputManager.instance.IsUseFixBow())
        {
            int hpP = playerCtrl.BowCtrl.GetHp();
            int maxhpP = playerCtrl.BowCtrl.GetHpMax();
            if (hpP < maxhpP)
            {
                SqwanItem(5);
            }
        }
    }
    void SqwanItem(int index)
    {
        foreach (var i in items)
        {
            if (i.itemDt.id == index)
            {
                if (i.soluong > 0)
                {
                    OtherItemsSqawner.Instance.Sqawn(i.itemDt.gameObjectPrefab.transform, transform.position, transform.rotation);
                    i.soluong--;
                }
            }

        }
    }
    public List<ItemOwner> GetItemOwners()
    {
        return items;
    }
}
