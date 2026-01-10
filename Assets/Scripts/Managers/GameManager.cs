using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGM.Singleton;
using DG.Tweening;


public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform startPoint;
    
    [Header("Animations")]
    public float duration = 1.2f;
    public float delay = .05f;
    public Ease ease = Ease.OutBack;

    private GameObject _currentPlayer;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawPlayer();
    }

    public void SpawPlayer()
    {
        _currentPlayer = Instantiate(playerPrefab);
        _currentPlayer.transform.position = startPoint.transform.position;
        _currentPlayer.transform.DOScale(0, duration).SetEase(ease).From().SetDelay(delay);

    }
}
