using UnityEngine;
using System.Collections.Generic;

public class Level_Manager : MonoBehaviour
{
    public bool onvalid;
    public void OnValidate()
    {
        if (onvalid)
        {
            onvalid = false;
            Transform way = transform.Find("Way");
            for (int i = 0; i < way.childCount; i++)
            {
                Transform forward = way.GetChild(i).Find("Forward");
                if (forward != null)
                {
                    forward.GetChild(1).localPosition = new Vector3(0, 0.6f, -0.17f);
                    forward.GetChild(2).localPosition = new Vector3(0, 0.6f, 0.17f);
                }
            }
        }
    }
    [SerializeField] private int gameTime = 120;
    [SerializeField] private int spawnLimit;
    [SerializeField] private float trainSpeed = 0.5f;
    [SerializeField] private float trainSpawnTime;
    [SerializeField] private List<Pooler> trains = new List<Pooler>();
    private float trainSpawnTimeNext;
    private bool startSpawn;
    private int totalTrain;
    private int correctTrain;
    private int enterStationTrain;
    private Transform startPoint;
    private Transform firstRoute;
    private void Start()
    {
        Canvas_Manager.Instance.spawnStart += Instance_spawnStart;
        // Find way and set startPoint.
        startPoint = transform.Find("Way").GetChild(0).GetChild(0);
        firstRoute = transform.Find("Way").GetChild(0).Find("Forward");
        Game_Manager.Instance.SetGameTime(gameTime);
    }
    private void Instance_spawnStart(object sender, System.EventArgs e)
    {
        // Start spawn.
        startSpawn = true;
    }
    private void Update()
    {
        if (startSpawn)
        {
            // Spawn train
            trainSpawnTimeNext += Time.deltaTime;
            if (trainSpawnTimeNext > trainSpawnTime)
            {
                trainSpawnTimeNext = 0;
                SpawnTrain();
                totalTrain++;
                // Set Spawned train amount.
                Canvas_Manager.Instance.SetTextSpawn(totalTrain, correctTrain);
                if (spawnLimit == totalTrain)
                {
                    startSpawn = false;
                }
            }
        }
    }
    private void SpawnTrain()
    {
        //(PoolObje, bool) train = trains[0].RequestGameObjectFromPool(startPoint.position);
        (PoolObje, bool) train = trains[Random.Range(0, trains.Count)].RequestGameObjectFromPool(startPoint.position);
        Train tr = train.Item1.GetComponent<Train>();
        tr.SetSpeed(trainSpeed);
        tr.SetRoute(firstRoute);
        if (train.Item2)
        {
            train.Item1.transform.SetParent(transform.parent);
        }
    }
    public void CorrectTrain()
    {
        correctTrain++;
        Canvas_Manager.Instance.SetTextSpawn(totalTrain, correctTrain);
    }
    public void EnterStation()
    {
        enterStationTrain++;
        if (enterStationTrain == spawnLimit) // Game Finish
        {
            // Game Win
            Canvas_Manager.Instance.GameFinish();
        }
    }
    public int LearnCorrect()
    {
        return correctTrain;
    }
}