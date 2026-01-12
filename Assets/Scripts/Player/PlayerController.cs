using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;

    [Header("Speed SetUp")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    private float _currentSpeed;


    [Header("Animation SetUp")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float landScaleY = 0.7f;
    public float landScaleX = 1.5f;
    public float animationDuration = 0.3f;
    public Ease ease = Ease.OutBack;
    public bool justLanded = false;


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
            _currentSpeed = speedRun;
        }
        else
        {
            _currentSpeed = speed;
        }

        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
        } 
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);            
        }  

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }
        else if(myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            myRigidbody.velocity = Vector2.up * forceJump;
            myRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
        } 
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }

    private void HandleScaleLanding()
    {
        if(justLanded)
        {
            myRigidbody.transform.DOScaleY(landScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
            myRigidbody.transform.DOScaleX(landScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!justLanded && !other.CompareTag("junpper")) // só roda se ainda não tinha "pousado"
        {
            justLanded = true;
            HandleScaleLanding();
            StartCoroutine(reScale());

        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        justLanded = false;
        
    }

    IEnumerator reScale()
    {
        yield return new WaitForSeconds(animationDuration + 0.2f);
        myRigidbody.transform.localScale = Vector3.one; // garante (1,1,1)
    }



}
