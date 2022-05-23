using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Canvas_Manager : MonoBehaviour
{
    // Exercise your divided attention by directing each train to its matching station
    public event System.EventHandler spawnStart;
    public event System.EventHandler gameFinish;
    #region Instance
    private static Canvas_Manager instance;
    public static Canvas_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI spawnText;
    [SerializeField] private TextMeshProUGUI gameLastTime;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform levelPanel;
    [SerializeField] private TextMeshProUGUI correctText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject returnObject;
    private List<Image> levelButtonImage = new List<Image>();

    private void Start()
    {
        // Set level button
        for (int h = 0; h < levelPanel.childCount; h++)
        {
            int level = h;
            levelButtonImage.Add(levelPanel.GetChild(h).GetComponent<Image>());
            levelPanel.GetChild(h).GetComponent<Button>().onClick.AddListener(() => SetLevelNumber(level));
        }
    }
    private void SetLevelNumber(int level)
    {
        levelButtonImage[Game_Manager.Instance.gameLevel].color = Color.white;
        // Decide level number
        Game_Manager.Instance.gameLevel = level;
        levelButtonImage[level].color = Color.red;
    }
    public void StartGame()
    {
        StartCoroutine(StartGameTime());
        Game_Manager.Instance.SetGameObjectPosition();
    }
    private IEnumerator StartGameTime()
    {
        int lastTime = 3;
        gameLastTime.text = lastTime.ToString();
        animator.SetTrigger("LastTime");
        while (lastTime > 0)
        {
            yield return new WaitForSeconds(1);
            lastTime--;
            gameLastTime.text = lastTime.ToString();
            animator.SetTrigger("LastTime");
        }
        yield return new WaitForSeconds(1);
        animator.SetTrigger("GameTime");
        spawnStart?.Invoke(this, System.EventArgs.Empty);
    }
    // Selected UI Panel anim
    public void GoSelectPanel(string selectName)
    {
        animator.SetTrigger(selectName);
    }
    // Game Time Function.
    public void SetTextTime(int time)
    {
        int minute = time / 60;
        int second = time - (60 * minute);
        timeText.text = "Time  " + minute + " : " + second;
    }
    // Spawn Train Function.
    public void SetTextSpawn(int totalTrain, int correctTrain)
    {
        spawnText.text = "Correct " + correctTrain + " of " + totalTrain;
    }
    // Game Win
    public void GameFinish()
    {
        animator.SetTrigger("GameFinish");
        gameFinish?.Invoke(this, System.EventArgs.Empty);
    }
    public void SetCorrectText(int correct)
    {
        correctText.text = "Correct : " + correct;
    }
    public void SetScoreText(int score)
    {
        scoreText.text = "Score : " + score;
    }
    public void SetLevelText(int level)
    {
        levelText.text = "Level : " + level;
    }
    public void ShowReturnButton()
    {
        returnObject.SetActive(true);
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Train");
    }
}