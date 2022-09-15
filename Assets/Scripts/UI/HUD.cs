using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    [Header("Game Session Info")]
    [SerializeField] private GameSession _gameSession = null;
    [SerializeField] private Text _scoreValue = null;
    [SerializeField] private GameObject _timeRemaining = null;
    [SerializeField] private Text _timeRemainingValue = null;
    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverScreen = null;
    private bool _sessionEnded;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameSession.OnSessionEnd += HandleSessionEnded;
        _gameSession.OnSessionStart += HandleSessionStarted;
    }

    private void HandleSessionStarted()
    {
        _sessionEnded = false;
        _timeRemaining.SetActive(true);
        _gameOverScreen.SetActive(false);
        StartCoroutine(UpdateUI());
    }

    IEnumerator UpdateUI()
    {
        while (!_sessionEnded)
        {
            _timeRemainingValue.text = GetFormattedTimeFromSeconds(_gameSession.timeLeft);
            // Update score value
            _scoreValue.text = _gameSession.totalScore.ToString();
            yield return new WaitForSeconds(1); // wait 1 sec
        }
    }

    string GetFormattedTimeFromSeconds( float seconds )
    {
        return Mathf.FloorToInt( seconds / 60.0f ).ToString("0") + ":" + Mathf.FloorToInt( seconds % 60.0f ).ToString("00");
    }

    void HandleSessionEnded()
    {
        _gameSession.OnSessionEnd -= HandleSessionEnded;
        _sessionEnded = true;
        _timeRemaining.SetActive(false);
        _gameOverScreen.SetActive(true);
    }
}
