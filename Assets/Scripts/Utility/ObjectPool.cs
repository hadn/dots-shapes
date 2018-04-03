using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
	public GameObject prefabToPool;
    List<ObjectPoolElement> pool;

    List<int> freeElementIndexes;

    void Awake()
    {
        GameObject clone = (GameObject)Instantiate(prefabToPool,Vector3.zero,Quaternion.identity,transform);
		clone.SetActive (false);

        pool = new List<ObjectPoolElement>();
        freeElementIndexes = new List<int>();

        for (int i = 0; i < transform.childCount; i++)
        {
            ObjectPoolElement element = transform.GetChild(i).GetComponent<ObjectPoolElement>();
            if (element == null)
                Debug.Log(transform.GetChild(i).name + " does not contain a ObjectPoolElement attached");
            else
            {
                pool.Add(element);
                freeElementIndexes.Add(i);
            }
        }
    }

    public int getActiveObjectCount()
    {
        return pool.Count - freeElementIndexes.Count;
    }

    public GameObject getObject()
    {
        int firstIndex = freeElementIndexes[0];
        freeElementIndexes.RemoveAt(0);

        if (freeElementIndexes.Count == 0)
        {
            GameObject duplicateThis = pool[firstIndex].gameObject;
            doubleCapacity(duplicateThis);
        }
        pool[firstIndex].activate();
        return pool[firstIndex].gameObject;
    }

    public void returnObject(GameObject elementGO)
    {
        ObjectPoolElement element = elementGO.GetComponent<ObjectPoolElement>();
        element.deactivate();
        freeElementIndexes.Add(pool.IndexOf(element));
    }

    void doubleCapacity(GameObject duplicateThis)
    {
        int poolCapacity = pool.Count;
        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject clone = (GameObject)Instantiate(duplicateThis);
            clone.name = duplicateThis.name;
            clone.transform.SetParent(duplicateThis.transform.parent);
            ObjectPoolElement clonePoolElement = clone.GetComponent<ObjectPoolElement>();
            clonePoolElement.copyTransform(duplicateThis);
            pool.Add(clonePoolElement);
            freeElementIndexes.Add(pool.IndexOf(clonePoolElement));
        }
    }

}
