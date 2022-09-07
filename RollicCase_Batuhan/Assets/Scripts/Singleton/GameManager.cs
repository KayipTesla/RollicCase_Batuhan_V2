using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] UIManager uiManager;
    [SerializeField] MapManager mapManager;

    public int finishPoint;
    public int totalPoint;

    public Vector3 nextLevelStartPosition;

    public GameObject mapOnPlay;
    public int mapPartCount;
    public int mapPartIndex;
    public int mapsLevel;
    public int throwForce;

    public enum GameState
    {
        TapToPlay,
        Play,
        Stop,
        StartClimbing,
        EndClimbing,
        NextLevel,
        Finish,
        Lose,
    }
    public GameState gameState;

    private void Awake()
    {
        instance = this;
        DatabaseCreate();
    }
    public void StartGame()
    {
        gameState = GameState.Play;
        uiManager.UIStartGame();
    }
    public void LevelPartInCrease()
    {
        mapPartIndex++;
    }
    public void NextLevel()
    {
        uiManager.UINextLevel();
        finishPoint = 0;
        gameState = GameState.TapToPlay;
    }
    public void GameOver()
    {
        uiManager.UIGameOver();
    }
    public void TryAgain()
    {
        gameState = GameState.Lose;
        uiManager.UITryAgain();
        mapManager.TryAgain();
    }
    public IEnumerator Finish()
    {
        gameState = GameState.Finish;
        FinishPointTransfer();
        yield return new WaitForSeconds(2f);
        NextLevelPositionCalculation();
        yield return new WaitForSeconds(3f);
        uiManager.UIFinish();
    }
    public void StartClimbingTheRamp()
    {
        gameState = GameState.StartClimbing;
        throwForce = 3000;
        StartCoroutine(ThrowForceCalculation());
        uiManager.UIStartClimbing();
        mapManager.NextLevelLoad();
    }
    private IEnumerator ThrowForceCalculation()
    {
        while (gameState == GameState.StartClimbing)
        {
            if (throwForce > 3000 )
            {
                throwForce -= 50;
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
    private void FinishPointTransfer()
    {
        totalPoint += finishPoint;
        finishPoint = 0;
        uiManager.UIFinisPointTransfer();
        
    }
    private void NextLevelPositionCalculation()
    {
        nextLevelStartPosition.z += 214;
        gameState = GameState.NextLevel;
        throwForce = 3000;
    }
    private void DatabaseUpdate()
    {
        PlayerPrefs.SetInt("mapsLevel", mapsLevel);
        PlayerPrefs.SetInt("totalPoint", totalPoint);
    }
    private void DatabaseCreate()
    {
        if (PlayerPrefs.HasKey("mapsLevel"))
        {
            mapsLevel = PlayerPrefs.GetInt("mapsLevel");
        }
        else
        {
            PlayerPrefs.SetInt("mapsLevel", mapsLevel);
        }
        if (PlayerPrefs.HasKey("totalPoint"))
        {
            totalPoint = PlayerPrefs.GetInt("totalPoint");
        }
        else
        {
            PlayerPrefs.SetInt("totalPoint", totalPoint);
        }
    }
    private void OnApplicationQuit()
    {
        DatabaseUpdate();
    }

}

