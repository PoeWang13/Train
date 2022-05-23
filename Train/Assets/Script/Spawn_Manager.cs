using UnityEngine;
using System.Collections.Generic;

public class Spawn_Manager : MonoBehaviour
{
    #region Instance
    private static Spawn_Manager instance;
    public static Spawn_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    [SerializeField] private Transform gameAreaObject;
    [SerializeField] private List<Level_Manager> levels = new List<Level_Manager>();
    private Level_Manager myLevel;
    public void CreateGameArea()
    {
        // Create game level.
        myLevel = Instantiate(levels[Game_Manager.Instance.gameLevel], gameAreaObject);
    }
    // Increase the number of trains going to the right station.
    public void CorrectTrain()
    {
        myLevel.CorrectTrain();
    }
    public void EnterStation()
    {
        myLevel.EnterStation();
    }
    public int LearnCorrect()
    {
        return myLevel.LearnCorrect();
    }
}