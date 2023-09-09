using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Items",menuName ="Data/Items")]
public class ItemData : ScriptableObject
{
    public List<ItemInfor> itemInfors;
    public GameObject itemUIShop;

}

[Serializable]
public class ItemInfor
{
    public int id;
    public string name;
    public Sprite icon;
    public int coint;
    public Type type;
    public string mota;
    public GameObject gameObjectPrefab;
}
public enum Type
{
    normal,
    arow
}
