using UnityEngine;
using System.Collections;

public class Train : PoolObje
{
    [SerializeField] private int myNumber;
    private Transform routes;
    private float tParam = 0f;
    private Vector3 objectPosition;
    private float speedModifier = 0.5f;
    private bool coroutineAllowed = true;
    public bool IsRightTrain(int trainNumber)
    {
        return myNumber == trainNumber;
    }
    // Set speedModifier
    public void SetSpeed(float speed)
    {
        speedModifier = speed;
    }
    // Update is called once per frame
    private void Update()
    {
        if (coroutineAllowed && routes != null)
        {
            StartCoroutine(GoByTheRoute());
        }
    }
    private IEnumerator GoByTheRoute()
    {
        coroutineAllowed = false;
        Vector3 p0 = routes.GetChild(0).position;
        Vector3 p1 = routes.GetChild(1).position;
        Vector3 p2 = routes.GetChild(2).position;
        Vector3 p3 = routes.GetChild(3).position;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            Vector3 dir = objectPosition - transform.position;
            Quaternion lookRotate = Quaternion.LookRotation(dir, Vector3.forward);
            Vector3 rotate = lookRotate.eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotate.y + 90, 0);
            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        coroutineAllowed = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Train_Way>(out Train_Way train_Way))
        {
            routes = train_Way.ReturnWay();
        }
    }
    public void SetRoute(Transform myRoute)
    {
        routes = myRoute;
    }
    public override void EnterGameObjectToPool()
    {
        base.EnterGameObjectToPool();
        tParam = 0f;
        coroutineAllowed = true;
    }
}