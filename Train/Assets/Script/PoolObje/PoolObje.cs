using UnityEngine;
public class PoolObje : MonoBehaviour
{
    [Tooltip("GameObject's pool for optimization")]
    public Pooler myPool;
    /// <summary>
    /// Call this function if you want to change something before entering pool.
    /// </summary>
    public virtual void EnterGameObjectToPool()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Call this function if you want to change something after exiting pool.
    /// </summary>
    public virtual void ExitGameObjectFromPool()
    {
        gameObject.SetActive(true);
    }
    public void EnterPool()
    {
        myPool.EnterGameObjectToPool(this);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}