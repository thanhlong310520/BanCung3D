using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sqawner : MyMonoBehaviour
{
    [SerializeField] protected List<Transform> prefabs;

    [SerializeField] protected List<Transform> poolObjs;
    [SerializeField] protected Transform holder;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHoder();
    }

    protected virtual void LoadHoder()
    {
        if(this.holder != null)
        {
            return;
        }
        this.holder = transform.Find("Holder");
        if(holder == null)
        {

            Debug.Log("khong tim thay holdel");
        }
        else
        {
            Debug.Log("da tim thay holdel");
        }
    }

    private void LoadPrefabs()
    {
        if(this.prefabs.Count > 0)
        {
            return;
        }
        Transform prefabObj = transform.Find("Prefabs");
        foreach(Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        HidePrefabs();
    }
    protected virtual void HidePrefabs()
    {
        foreach(Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Sqawn(string prefabName,Vector3 spawnPos,Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
            return null;
        return Sqawn(prefab,spawnPos,rotation);

    }
    public virtual Transform Sqawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.transform.position = spawnPos;
        newPrefab.transform.rotation = rotation;
        newPrefab.parent = this.holder;
        newPrefab.gameObject.SetActive(true);
        return newPrefab;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach(Transform obj in this.poolObjs)
        {
            if(obj.name == prefab.name)
            {
                this.poolObjs.Remove(obj);
                return obj;
            }
        }
        Transform newObj = Instantiate(prefab);
        newObj.name = prefab.name;
        return newObj;
    }

    public virtual Transform GetPrefabByName(string name)
    {
        foreach(Transform prefab in prefabs)
        {
            if(prefab.name == name)
            {
                return prefab;
            }
        }
        return null;
    }

    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
