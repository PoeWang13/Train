using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class RailWay
{
    public Transform controlPoint;
    public GameObject railWay;
}
public class Train_Way : MonoBehaviour
{
    [SerializeField] private RailWay[] railWays;
    public RailWay[] RailWays { get { return railWays; } }
    private Vector3 gizmosPosition;
    private int choosingWay;
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    // Get rid of unnecessary objects.
    public void DeleteUnnecessaryObject()
    {
        if (railWays.Length == 1)
        {
            for (int e = transform.childCount - 1; e > 0; e--)
            {
                if (transform.GetChild(e) == railWays[0].controlPoint)
                {
                    continue;
                }
                else if (transform.GetChild(e) == railWays[0].railWay.transform)
                {
                    continue;
                }
                else
                {
                    DestroyImmediate(transform.GetChild(e).gameObject);
                }
            }
        }
    }
    public void SetRailWayActivited()
    {
        for (int h = 0; h < transform.childCount; h++)
        {
            transform.GetChild(h).gameObject.SetActive(false);
        }
        railWays[1 % railWays.Length].railWay.SetActive(true);
    }
    private void OnMouseDown()
    {
        railWays[choosingWay].railWay.SetActive(false);
        choosingWay++;
        if (choosingWay == railWays.Length)
        {
            choosingWay = 0;
        }
        railWays[choosingWay].railWay.SetActive(true);
    }
    public Transform ReturnWay()
    {
        return railWays[choosingWay].controlPoint;
    }
    private void OnDrawGizmos()
    {
        if (railWays[choosingWay].controlPoint != null)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * railWays[choosingWay].controlPoint.GetChild(0).position +
                    3 * Mathf.Pow(1 - t, 2) * t * railWays[choosingWay].controlPoint.GetChild(1).position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * railWays[choosingWay].controlPoint.GetChild(2).position +
                    Mathf.Pow(t, 3) * railWays[choosingWay].controlPoint.GetChild(3).position;

                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
            Gizmos.DrawLine(new Vector3(railWays[choosingWay].controlPoint.GetChild(0).position.x, railWays[choosingWay].controlPoint.GetChild(0).position.y), new Vector3(railWays[choosingWay].controlPoint.GetChild(1).position.x, railWays[choosingWay].controlPoint.GetChild(1).position.y));
            Gizmos.DrawLine(new Vector3(railWays[choosingWay].controlPoint.GetChild(2).position.x, railWays[choosingWay].controlPoint.GetChild(2).position.y), new Vector3(railWays[choosingWay].controlPoint.GetChild(3).position.x, railWays[choosingWay].controlPoint.GetChild(3).position.y));
        }
    }
}