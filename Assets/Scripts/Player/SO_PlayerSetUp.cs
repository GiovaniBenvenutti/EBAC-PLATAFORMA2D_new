using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "SO_PlayerSetUp", menuName = "ScriptableObjects/SO_PlayerSetUp", order = 1)]
public class SO_PlayerSetUp : ScriptableObject
{
    public Animator player;
    public SOString soStringName;

    [Header("Speed SetUp")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    public float _currentSpeed;


    [Header("Animation SetUp")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float landScaleY = 0.7f;
    public float landScaleX = 1.5f;
    public float animationDuration = 0.3f;



    public Ease ease = Ease.OutBack;
    public bool justLanded = false;

    [Header("Animation Player")]
    public string runAnim = "RUN";
    public string deathAnim = "DEATH";
    public float playerSwipDuration = 0.05f;
}
