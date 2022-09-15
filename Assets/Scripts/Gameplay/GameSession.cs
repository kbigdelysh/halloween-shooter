using Assets.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public System.Action OnSessionStart;
    public System.Action OnSessionEnd;
    [HideInInspector]
    public float timeLeft = 0;
    public int totalScore = 0; // current score obtained so far in the session.

    public enum SessionState
    {
        Paused,
        Active,
        Finished
    }

    private SessionState _state = SessionState.Paused;

    // Start is called before the first frame update
    void Start()
    {
        StartSession();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == SessionState.Active)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                EndSession();
            }
        }
    }


    void StartSession()
    {
        _state = SessionState.Active;
        timeLeft = Config.instance.GetGameSessionLengthInSec();

        if (OnSessionStart != null)
        {
            OnSessionStart();
        }
    }

    void EndSession()
    {
        UpdateHighScore();

        if (OnSessionEnd != null)
        {
            OnSessionEnd();
        }
    }
    /// <summary>
    /// Updates the highest achieved score so far among sessions.
    /// </summary>
    private void UpdateHighScore()
    {
        // Update the high score in the player setting If the current session score is higher.
        if (!PlayerPrefs.HasKey("highScore"))
            PlayerPrefs.SetInt("highScore", 0);

        if (PlayerPrefs.GetInt("highScore") < totalScore)
        {
            PlayerPrefs.SetInt("highScore", totalScore);
            PlayerPrefs.Save();
        }
    }

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
