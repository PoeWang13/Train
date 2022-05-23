using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Pool")]
public class Pooler : ScriptableObject
{
    #region GameObject
    [Header("Creating GameObje")]
    public PoolObje prefab;
    public Queue<PoolObje> pool = new Queue<PoolObje>();
    public void EnterGameObjectToPool(PoolObje pool)
    {
        if (pool == null)
        {
            return;
        }
        // Set Gameobje before sending pool.
        pool.EnterGameObjectToPool();
        // Sending pool.
        this.pool.Enqueue(pool);
    }
    /// <summary>
    /// Request GameObje from pool. 
    /// </summary>
    /// <param name="pos">Instantiate or showing point</param>
    /// <returns>True is Gameobje Instantiated. False is pool sending old gameobject</returns>
    public (PoolObje, bool) RequestGameObjectFromPool(Vector3 pos)
    {
        PoolObje havuzObjesi = null;
        bool isNew = true;
        // If there is a usable object in the pool, use it.
        if (pool.Count > 0)
        {
            havuzObjesi = pool.Dequeue();
            if (havuzObjesi == null)
            {
                // If the available object was deleted after it was added to the pool.
                havuzObjesi = Instantiate(prefab, pos, Quaternion.identity);
            }
            else
            {
                // Edit available object.
                havuzObjesi.transform.position = pos;
                havuzObjesi.ExitGameObjectFromPool();
                isNew = false;
            }
            return (havuzObjesi, isNew);
        }
        // If there is no available object, build a new object.
        havuzObjesi = Instantiate(prefab, pos, Quaternion.identity);
        // Submit the new object.
        return (havuzObjesi, isNew);
    }
    #endregion
}