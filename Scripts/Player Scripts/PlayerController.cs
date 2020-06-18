using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float normalMoveSpeed = 7;
    [SerializeField] private float normalJumpHeight = 10;

    [SerializeField] private float hyperMoveSpeed = 5;
    [SerializeField] private float hyperJumpHeight = 5;
    
    [SerializeField] private float maxVelocity = 5;
    
    [HideInInspector] public bool inHyperSpeed;
    
    private float currentMoveSpeed = 7;
    private float currentJumpHeight = 10;

    private new Rigidbody2D rigidbody;
    private Vector2 playerSize;
    private Vector2 colliderBoxSize;

    private const float colliderBoxThickness = 0.5f;
    public LayerMask groundMask;


    [HideInInspector] public float fallScale = 1f;
    private float fallSpeed = 9.8f;

    [SerializeField] private GameObject ghost = null;
    
    // Made public so the ghosts can take the form of current sprite
    [HideInInspector] public SpriteRenderer spriteRenderer;
    
    // Made public so the spark particles know when to emit
    [HideInInspector] public bool onGround = false;
    private bool jumpRequest = false;

    private bool withinSweeper = false;
    
    private Vector3 inputDirection;

    public static Vector3 checkpointPosition;
    
    private Animator animator;

    [SerializeField] private float maxTimeInAir = 1.0f;
    private float airTimer;
    private float speedBurstCoolDownTimer = 0;

    private HyperSpeedManager hyperSpeedManager;

    private Vector3 lastPosition;

    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
 
    }
    void Awake()
    {
        lastPosition = transform.position;
        airTimer = maxTimeInAir;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // player size is the size of the player's box collider size +  the box collider edge radius
        playerSize = GetComponent<BoxCollider2D>().size + Vector2.one * GetComponent<BoxCollider2D>().edgeRadius;
        if(PlayerController.checkpointPosition != Vector3.zero)
            transform.localPosition = PlayerController.checkpointPosition;
        colliderBoxSize = new Vector2(playerSize.x, colliderBoxThickness);

    }
    
    

    void Update()
    {
        // if fallen below the bottom of the level, the player has died.
        if (transform.position.y < -20)
        {
            //Stop music and play death sound
            try
            {
                FindObjectOfType<AudioManager>().Stop("song");
                FindObjectOfType<AudioManager>().Play("death");
            } catch (Exception e)
            {
                Debug.Log("No Audio Objects Found " + e);
            }
            finally
            {
                Destroy(this.gameObject);
                SceneManager.Instance.playerDeath(0.5f);
            }
        }
        
        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (onGround || !inHyperSpeed)
        {
            Vector2 perpVec = Vector2.Perpendicular(transform.rotation * Vector2.up);
            inputDirection = (Vector2.Dot(perpVec, inputDirection) / Vector2.SqrMagnitude(perpVec)) * perpVec;
        }
        
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
        else
        {
       //     jumpRequest = false;

        }
        
        
        // Turn the player to face the direction they're moving
      
        // if the current angle is not evenly divisible by 180 (aka Jono is sideways)
        if (!(Math.Abs(Mathf.Abs(transform.eulerAngles.z)%180) < 0.01))
        {
            float rotCorrecter = Mathf.Abs(transform.eulerAngles.z) == 270 ? -1 : 1;

            if (inputDirection.y < 0)
                transform.localScale = new Vector3(-1 * rotCorrecter, transform.localScale.y ,1);
            if (inputDirection.y > 0)
                transform.localScale = new Vector3(1 * rotCorrecter, transform.localScale.y,1);
        }
        else
        {
            float rotCorrecter = Mathf.Abs(transform.eulerAngles.z) == 180 ? -1 : 1;
            
            if (inputDirection.x < 0)
                transform.localScale = new Vector3(-1 * rotCorrecter, transform.localScale.y ,1);
            if (inputDirection.x > 0)
                transform.localScale = new Vector3(1 * rotCorrecter, transform.localScale.y,1);
            
        }
        
        inHyperSpeed = Input.GetButton("Hyperspeed") && !hyperSpeedManager.blocked;

        speedBurstCoolDownTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Hyperspeed") && speedBurstCoolDownTimer<= 0)
        {
            rigidbody.velocity +=  (Vector2)inputDirection*6;
            speedBurstCoolDownTimer = 1;
        }
        
        if (inHyperSpeed)
        {
            currentMoveSpeed = hyperMoveSpeed;
            currentJumpHeight = hyperJumpHeight;
            Instantiate(ghost, Vector3.zero, transform.rotation);
        } else
        {
            currentMoveSpeed = normalMoveSpeed;
            currentJumpHeight = normalJumpHeight;
        }



        animator.SetBool("Jumping_Rising", rigidbody.velocity.y > 0.01);
        animator.SetBool("Jumping_Rising", false);

        animator.SetBool("Jumping_Falling", !onGround);


    }

    void FixedUpdate()
        {

        Vector3 positionDifference = lastPosition - transform.position;
        lastPosition = transform.position;

        // Animator parameters
        if (onGround)
            animator.SetFloat("X-Speed", positionDifference.magnitude * 1000);
        else
            animator.SetFloat("X-Speed", 0f);
        if (inHyperSpeed)
            {
                if (onGround)
                {
                    airTimer = maxTimeInAir;
                    rigidbody.drag = 0.35f;
                    if (!withinSweeper)
                    {
                        hyperSpeedManager.blocked = false;
                    }
                }
                else
                {
                    airTimer -= Time.deltaTime;
                    airTimer = Mathf.Clamp(airTimer,0, maxTimeInAir);
                    float airTime01 =  airTimer / maxTimeInAir;
                    // multiplier goes from 1 to 5 as airTimer goes to 0
                    rigidbody.drag = 0.35f * ( (1 - airTime01*airTime01)*4.0f + 1.0f);
//                    HyperSpeedManager.Instance.
                    if (airTime01 < 0.01) hyperSpeedManager.blocked = true;
                }
            }
            else
            {
                if (onGround)
                    rigidbody.drag = 3;
                else 
                    rigidbody.drag = 0;
            if (!withinSweeper)
            {
                hyperSpeedManager.blocked = !onGround;
            }
            }
            
            // input movement
            if (!inHyperSpeed)
                rigidbody.transform.Translate(currentMoveSpeed * Time.deltaTime * inputDirection, Space.World);
            else 
                rigidbody.AddForce(currentMoveSpeed * inputDirection);
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxVelocity);

            Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

            // Gravity
            rigidbody.AddForce(fallSpeed * fallScale * new Vector2(0, -1));
              
            var localScale = transform.localScale;
            // If player can jump and wants to jump, then jump
            if (jumpRequest && onGround)
            {
                Vector3 jumpDirection = rotation * new Vector3(0, localScale.y, 0);
                rigidbody.AddForce(currentJumpHeight * jumpDirection, ForceMode2D.Impulse); 
                jumpRequest = false;
                onGround = false;
            // else check if a box beneath the player's feet is overlapping with ground. If it is, then the player can jump
            } else
            {
                // the center of the box as at the players position + half the player's height towards the player's feet + half the box height.
                Vector2 boxCenter = (Vector2) transform.position + (playerSize.y + colliderBoxSize.y) * localScale.y * 0.5f * (Vector2)(rotation * Vector2.down);
                onGround = (Physics2D.OverlapBox(boxCenter, colliderBoxSize, transform.eulerAngles.z, groundMask) != null);
            }

           
            
            
            // if the player's in the air and in hyper speed
            if (inHyperSpeed)
            {
                // same as above except with Vector2.up to test if there's ground above the player's head
                Vector2 boxCenter = (Vector2) transform.position + (playerSize.y + colliderBoxSize.y) * localScale.y  * 0.5f * (Vector2)(rotation * Vector2.up);
                bool groundAbove = (Physics2D.OverlapBox(boxCenter, colliderBoxSize, transform.eulerAngles.z, groundMask) != null);

                
                Vector2 sideboxSize = new Vector2(colliderBoxSize.y*1f, colliderBoxSize.x);
                boxCenter = (Vector2) transform.position + (playerSize.x + sideboxSize.x) * localScale.x * 0.5f * (Vector2)(rotation * Vector2.right);
                bool groundInFront = (Physics2D.OverlapBox(boxCenter, sideboxSize, transform.eulerAngles.z, groundMask) != null);
                
                // if there is ground above the player, flip their local scale so they're upside down.
                if (groundAbove && !onGround)
                {
                    transform.localScale *= new Vector2(1, -1);
//                    transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z%180);
                    
//                    Vector2 perpVec = Vector2.left;
//                    Vector2 paraVel = (Vector2.Dot(perpVec, rigidbody.velocity) / Vector2.SqrMagnitude(perpVec)) * perpVec;
//                    rigidbody.velocity = (Vector2)(Quaternion.Euler(0, 0, 180) * paraVel);

                } else if (groundInFront)
                {
                    Vector2 perpVec = Vector2.Perpendicular(transform.rotation * Vector3.up);
                    Vector2 paraVel = (Vector2.Dot(perpVec, rigidbody.velocity) / Vector2.SqrMagnitude(perpVec)) * perpVec;
                    if (transform.localScale.x == -1)
                    {
                        transform.eulerAngles += new Vector3(0, 0, -90);
                        rigidbody.velocity += (Vector2)(Quaternion.Euler(0, 0, -90) * paraVel);
                    }
                    else
                    {
                        transform.eulerAngles += new Vector3(0, 0, 90);
                        rigidbody.velocity += (Vector2)(Quaternion.Euler(0, 0, 90) * paraVel);
                    }
                }
                
                  
            }
            // The second the player isn't in hyper speed set everything back to normal
            else if (!inHyperSpeed)
            {
                localScale.y = 1;
                transform.localScale = localScale;
                transform.eulerAngles = new Vector3(0, 0,0);
            }
        }
    
    // Draw the two boxes used above for ground testing when the gizmos are enabled.
    void OnDrawGizmos() {
        
        Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);

        
        Gizmos.color = Color.blue;
        Vector3 boxCenter =  transform.position + (playerSize.y * 0.5f + colliderBoxSize.y) * transform.localScale.y * (rotation * Vector3.down);
        Gizmos.DrawWireCube(boxCenter, new Vector3(colliderBoxSize.x, colliderBoxSize.y, 1));
        
        Gizmos.color = Color.red;
        boxCenter =  transform.position + (playerSize.y * 0.5f + colliderBoxSize.y) * transform.localScale.y  * (rotation * Vector3.up);
        Gizmos.DrawWireCube(boxCenter, new Vector3(colliderBoxSize.x, colliderBoxSize.y, 1));
        Gizmos.color = Color.cyan;
        Vector2 sideboxSize = new Vector2(colliderBoxSize.y*1f, colliderBoxSize.x);
        boxCenter = (Vector2) transform.position + (playerSize.x + sideboxSize.x) * transform.localScale.x * 0.5f * (Vector2)(rotation * Vector2.right);
        Gizmos.DrawWireCube ( boxCenter, new Vector3(sideboxSize.x, sideboxSize.y, 1));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Checkpoint")
        {
            checkpointPosition = collision.gameObject.transform.position;
            Debug.Log("Checkpoint Saved: " + checkpointPosition);

        }
        if (collision.gameObject.name == "Sweeper")
        {
            hyperSpeedManager.blocked = true;
            withinSweeper = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sweeper")
        {
            withinSweeper = false;
        }
    }


}
