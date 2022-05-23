using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour
{
    #region Instance
    private static Game_Manager instance;
    public static Game_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    [HideInInspector] public int gameLevel;
    private int gameTime;
    [SerializeField] private Transform gameObjectsParent;
    private float time;
    private bool startSpawn;
    private bool pullGameObject;
    private bool sendGameObject;
    private Vector3 orjGamePosition;
    private bool setScore;
    private void Start()
    {
        Canvas_Manager.Instance.spawnStart += SpawnStart;
        Canvas_Manager.Instance.gameFinish += GameFinish;
        // Set Game scale
        Vector3 levelLimit = Vector3.one;
        float targetRatio = 1920.0f / 1080;
        float screenRatio = (float)Screen.width / (float)Screen.height;
        ScreenFixing.SetScreenFixing(targetRatio, screenRatio);
        levelLimit = ScreenFixing.MyScreenRatio();
        gameObjectsParent.localScale = levelLimit;
        orjGamePosition = gameObjectsParent.position;
    }
    private void SpawnStart(object sender, System.EventArgs e)
    {
        startSpawn = true;
    }
    private void GameFinish(object sender, System.EventArgs e)
    {
        sendGameObject = true;
    }
    public void SetGameTime(int game)
    {
        gameTime = game;
        Canvas_Manager.Instance.SetTextTime(gameTime);
    }
    private void Update()
    {
        // Start 
        if (startSpawn)
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                time = 0;
                gameTime--;
                Canvas_Manager.Instance.SetTextTime(gameTime);
            }
        }
        if (pullGameObject)
        {
            int movingSpeed = 150;
            gameObjectsParent.position = Vector3.MoveTowards(gameObjectsParent.position, Vector3.zero, Time.deltaTime * movingSpeed);
            if (gameObjectsParent.position.x <= 0)
            {
                pullGameObject = false;
            }
        }
        if (sendGameObject)
        {
            int movingSpeed = 150;
            gameObjectsParent.position = Vector3.MoveTowards(gameObjectsParent.position, orjGamePosition, Time.deltaTime * movingSpeed);
            if (gameObjectsParent.position.x >= orjGamePosition.x)
            {
                sendGameObject = false;
                setScore = true;
            }
        }
        if (setScore)
        {
            setScore = false;
            int correct = Spawn_Manager.Instance.LearnCorrect();
            Canvas_Manager.Instance.SetCorrectText(correct);
            Canvas_Manager.Instance.SetScoreText(correct * 100);
            Canvas_Manager.Instance.SetLevelText(1);
            SetScoreTextWithLevelBonus(correct);
        }
    }
    // Send game object to Vector.zero 
    public void SetGameObjectPosition()
    {
        // Game Objects Parent
        pullGameObject = true;
    }
    private void SetScoreTextWithLevelBonus(int correct)
    {
        StartCoroutine(EarnLevelBonus(correct));
    }
    IEnumerator EarnLevelBonus(int correct)
    {
        yield return new WaitForSeconds(1);
        if (gameLevel != 0)
        {
            int lvl = 1;
            while (lvl < gameLevel + 1)
            {
                lvl++;
                Canvas_Manager.Instance.SetLevelText(lvl);
                Canvas_Manager.Instance.SetScoreText(correct * lvl * 100);
                yield return new WaitForSeconds(0.15f);
            }
        }
        Canvas_Manager.Instance.ShowReturnButton();
    }
}