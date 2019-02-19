using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectPool : MonoBehaviour
{

    class InstanceSet {
        public HashSet<GameObject> Unused = new HashSet<GameObject>();
        public HashSet<GameObject> Used = new HashSet<GameObject>();
    }
    
    private Dictionary<GameObject, InstanceSet> prefabToInstanceSet =
        new Dictionary<GameObject, InstanceSet>();

    private Dictionary<GameObject, InstanceSet> instanceToInstanceSet =
        new Dictionary<GameObject, InstanceSet>();
    
    private Dictionary<GameObject, GameObject> instanceToPrefab = new Dictionary<GameObject, GameObject> ();

    public void Free(GameObject instance)
    {
        instance.SetActive(false);
        instance.transform.SetParent(null);
        if (!instanceToInstanceSet.ContainsKey(instance))
        {
            //Instance is not handled by object pool. Delete it instead
            Destroy(instance);
        }
        else
        {
            var instanceList = instanceToInstanceSet[instance];
            instanceList.Used.Remove(instance);
            instanceList.Unused.Add(instance);
        }
    }

    public GameObject GetFreeInstance(GameObject sourceAsset) {

        GameObject prefab = null;
        if (sourceAsset.scene.rootCount != 0)
        {
            //Source asset is not a prefab. Try to find prefab
            if (instanceToPrefab.ContainsKey(sourceAsset))
            {
                prefab = instanceToPrefab[sourceAsset];
            }
            else {
                //Cannot find prefab, use the source object as prefab
                prefab = sourceAsset;
            }
        }
        else {
            prefab = sourceAsset;
        }
        Assert.IsNotNull(prefab);
        
        if (!prefabToInstanceSet.ContainsKey(prefab))
        {
            prefabToInstanceSet.Add(prefab, new InstanceSet());
        }

        var instanceSet = prefabToInstanceSet[prefab];
        var objectList = instanceSet.Unused;
        
        if (objectList.Count == 0) {
            var newInstance = Instantiate(prefab);
            objectList.Add(newInstance);
            instanceToInstanceSet.Add(newInstance, instanceSet);
            instanceToPrefab.Add(newInstance, prefab);
        }

        var listEnum = objectList.GetEnumerator();
        listEnum.MoveNext();
        var instance = listEnum.Current;
        Assert.IsNotNull(instance);

        objectList.Remove(instance);
        instanceSet.Used.Add(instance);

        instance.SetActive(true);
        instance.SendMessage("Start");

        return instance;
    }
}
