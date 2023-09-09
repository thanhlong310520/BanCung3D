using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerData 
{
    private const string Name_Save = "Player_save";
    static AllData allData;
    static UnityEvent updateCoint = new UnityEvent();
    static PlayerData()
    {
        allData = JsonUtility.FromJson<AllData>(PlayerPrefs.GetString(Name_Save));
        if(allData == null)
        {
            allData = new AllData
            {
                items = new List<ItemOwner>(),
                coint = 1000
            };
        }
    }
    public static List<ItemOwner> GetData()
    {
        return allData.items;
    }
    public static void ClearData()
    {
        allData.items.Clear();
        SaveData();
    }
    private static void SaveData()
    {
        var data = JsonUtility.ToJson(allData);
        PlayerPrefs.SetString(Name_Save, data);
    }
    public static void AddItem(ItemInfor i,int number)
    {
        allData.AddIdItemToList(i,number);
        SaveData();
    }
    public static void SaveOwner(List<ItemOwner> list)
    {
        allData.SaveHad(list);
        SaveData();
    }
}

public class AllData
{
    public int coint;
    public List<ItemOwner> items;
    public void AddIdItemToList(ItemInfor i,int nb)
    {
        bool ishas = false;
        foreach (var a in items)
        {
            if (a.itemDt.id == i.id)
            {
                ishas = true;
                a.soluong+=nb;
            }
        }
        if (!ishas)
        {
            items.Add(new ItemOwner(i,nb));
            items.Sort();
        }
    }
    public void SaveHad(List<ItemOwner> list)
    {
        foreach (var nw in list)
        {
            bool ishas = false;
            foreach (var it in items)
            {
                if(it.itemDt.id == nw.itemDt.id)
                {
                    it.soluong = nw.soluong;
                }
            }
            if (!ishas)
            {
                items.Add(new ItemOwner(nw.itemDt, nw.soluong));
                items.Sort();
            }
        }
    }
}
[Serializable]
public class ItemOwner : IComparable<ItemOwner>
{
    public ItemInfor itemDt;
    public int soluong;
    public ItemOwner(ItemInfor i,int nb)
    {
        itemDt = i;
        soluong = nb;
    }

    public int CompareTo(ItemOwner other)
    {
        return this.itemDt.id.CompareTo(other.itemDt.id);
    }
}