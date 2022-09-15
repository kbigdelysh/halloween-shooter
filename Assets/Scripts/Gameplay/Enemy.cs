using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int DESTROY_WAIT = 5; // [sec]
    [Header("Score")]
    public int scoreValue = 100;

    [Header("Movement")]
    [SerializeField] private float _verticalAmplitude = 2.5f;
    [SerializeField] private float _verticalFrequency = 2.5f;

    [Header("Physics")]
    [SerializeField] private Rigidbody _rigidBody = null;
    private GameSession _currentSession;
    private Vector3 _startPosition = Vector3.zero;
    public bool alive = true;
    public EnemySpawner enemySpawner; // This is set by the spawner.

    // Start is called before the first frame update
    void Start()
    {
        _currentSession = FindObjectOfType<GameSession>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float positionOffset = Mathf.Sin(Time.timeSinceLevelLoad / _verticalFrequency) * _verticalAmplitude;
            transform.position = new Vector3(_startPosition.x, _startPosition.y + positionOffset, _startPosition.z);
        }
    }

    void Die()
    {
        if (!alive) return; // if it's already dead, do nothing.
        _rigidBody.useGravity = true;
        // Update session score.
        _currentSession.totalScore += scoreValue;
        alive = false;
        enemySpawner.OnEnemyDeath();
        Destroy(gameObject, DESTROY_WAIT);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Cannonball>())
        {
            _rigidBody.AddForceAtPosition(collision.transform.forward, collision.GetContact(0).point, ForceMode.Impulse);
            Die();
        }
    }
}
