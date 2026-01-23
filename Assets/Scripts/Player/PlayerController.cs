using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    private Animator _currentPlayer;

    public SO_PlayerSetUp soPlayerSetUp;
    public HealthBase healthBase;

    void Awake()
    {
        healthBase = GetComponent<HealthBase>();

        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetUp.player, transform);
        
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetUp.deathAnim);
    }
    

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            soPlayerSetUp._currentSpeed = soPlayerSetUp.speedRun;
            _currentPlayer.speed = 1.5f;
        }
        else
        {
            soPlayerSetUp._currentSpeed = soPlayerSetUp.speed;
            _currentPlayer.speed = 0.8f;
        }

        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            myRigidbody.velocity = new Vector2(-soPlayerSetUp._currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x !=-1)
            {
                myRigidbody.transform.DOScaleX(-1, soPlayerSetUp.playerSwipDuration);
            }
            _currentPlayer.SetBool(soPlayerSetUp.runAnim, true);
        } 
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(soPlayerSetUp._currentSpeed, myRigidbody.velocity.y);
            if(myRigidbody.transform.localScale.x !=1)
            {
                myRigidbody.transform.DOScaleX(1, soPlayerSetUp.playerSwipDuration);
            }
            _currentPlayer.SetBool(soPlayerSetUp.runAnim, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetUp.runAnim, false);
        }

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= soPlayerSetUp.friction;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += soPlayerSetUp.friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            myRigidbody.velocity = Vector2.up * soPlayerSetUp.forceJump;
            myRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
        } 
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(soPlayerSetUp.jumpScaleY, soPlayerSetUp.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetUp.ease);
        myRigidbody.transform.DOScaleX(soPlayerSetUp.jumpScaleX, soPlayerSetUp.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetUp.ease);
    }

    private void HandleScaleLanding()
    {
        if(soPlayerSetUp.justLanded)
        {
            myRigidbody.transform.DOScaleY(soPlayerSetUp.landScaleY, soPlayerSetUp.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetUp.ease);
            myRigidbody.transform.DOScaleX(soPlayerSetUp.landScaleX, soPlayerSetUp.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetUp.ease);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!soPlayerSetUp.justLanded && !other.CompareTag("junpper")) // só roda se ainda não tinha "pousado"
        {
            soPlayerSetUp.justLanded = true;
            HandleScaleLanding();
            StartCoroutine(reScale());

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        soPlayerSetUp.justLanded = false;
        
    }

    IEnumerator reScale()
    {
        yield return new WaitForSeconds(soPlayerSetUp.animationDuration + 0.2f);
        myRigidbody.transform.localScale = Vector3.one; // garante (1,1,1)
    }



}
