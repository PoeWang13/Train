using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Train_Station : MonoBehaviour
{
    [SerializeField] private int trainNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Train>(out Train train))
        {
            if (train.IsRightTrain(trainNumber))
            {
                Spawn_Manager.Instance.CorrectTrain();
            }
            Spawn_Manager.Instance.EnterStation();
            train.EnterPool();
        }
    }
}