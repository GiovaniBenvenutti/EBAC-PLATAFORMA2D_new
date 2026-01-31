using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public HealthBase healthBase;

    [Header("Player SetUp")]
    public SO_PlayerSetUp soPlayerSetUp;

    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = 0.2f;

    [Header("Particles VFX")]
    public ParticleSystem jumpVFX;
    public ParticleSystem walkVFX;


    void Awake()
    {
        healthBase = GetComponent<HealthBase>();

        if(healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetUp.player, transform);

        if(collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
        
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetUp.deathAnim);
    }
    

    // Update is called once per frame
    void Update()
    {
        isGrounded();
        HandleJump();
        HandleMoviment();

        if(isGrounded() && myRigidbody.velocity.magnitude > 0.1f)
        {
            if(!walkVFX.isPlaying)
            {
                walkVFX.Play();
            }
        }
        else
        {
            if(walkVFX.isPlaying)
            {
                walkVFX.Stop();
            }
        }
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
        // se trocar o isGrounded pelo soPlayerSetUp.justLanded funciona tbm, 
        // só deiei assim pra ficar igual o do curso
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) 
        {
            myRigidbody.velocity = Vector2.up * soPlayerSetUp.forceJump;
            myRigidbody.transform.localScale = Vector2.one;
            DOTween.Kill(myRigidbody.transform);

            HandleScaleJump();
            PlayJumpVFX();
        } 
    }

    private void PlayJumpVFX()
    {
        if(jumpVFX != null)
        {
            VFXManager.Instance.PlayVfxByType(VFXManager.VFXType.JUMP, transform.position);
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
        if (!soPlayerSetUp.justLanded && other.CompareTag("floor")) // só roda se ainda não tinha "pousado"
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
