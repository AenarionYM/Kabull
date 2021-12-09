using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Object
    private Rigidbody2D _kabullBody;
    public Animator animator;
    public Transform wallCheck;
    public Transform isGroundedCheckerLeft;
    public Transform isGroundedCheckerRight;
    public LayerMask groundLayer;
    public Collider2D standingCollider;
    public Collider2D crouchingCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite sd;
    public Sprite ds;

    
    //Variables
    public static float movementSpeed = 10f;
    public float jumpForce = 6f;
    private float _moveBy;
    public float falllMultiplaier = 2.5f;
    public float lowJumpMultiplaier = 2f;
    public float checkGroundRadius;
    public float checkWallRadius;
    public static bool isOnWall = false;
    private bool _wallAvaliable = true;
    public int wallCooldownTime = 3;
    public static bool canMove = true;
    private bool _isGrounded = true;
    private bool _isCrouched = false;
    private float rememberGroundFor = 1f;
    private float _lastTimeGrounded;
    public int defaultAdditionalJumps = 1;
    private int _additionalJumps;
    public static bool facingRight;
    private bool _buttonDown = false;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int _OnGround = Animator.StringToHash("onGround");
    private static readonly int Crouched = Animator.StringToHash("Crouched");
    private static readonly int OnGround = Animator.StringToHash("OnGround");
    
    void Start()
    {
        _kabullBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            _buttonDown = false;
        }
    }
    
    void FixedUpdate()
    {
        float horizontalSpeed = Input.GetAxisRaw("Horizontal") * movementSpeed;
        
        CheckIfGrounded();
        if (canMove)
        {
            CheckIfOnWall();
            Move();
            Jump();
            BettererJump();
            Crouch();
            
            animator.SetFloat(Speed, Mathf.Abs(horizontalSpeed));
            animator.SetBool(Crouched, _isCrouched);
            animator.SetBool(OnGround, _isGrounded);
        }
        else
        {
            animator.SetFloat(Speed, 0);
            animator.SetBool(Crouched, false);
            animator.SetBool(OnGround, _isGrounded);
        }
    }
    
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButton("Crouch"))
        {
            _moveBy = x * movementSpeed;
            _moveBy = _moveBy * 0.5f;
        }
        else
        {
             _moveBy = x * movementSpeed;
        }

        _kabullBody.velocity = new Vector2(_moveBy, _kabullBody.velocity.y);
        
        if (_moveBy < 0 && facingRight)
        {
            Flip();
        }
        else if (_moveBy > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

    void Jump()
    {
        if (Input.GetButton("Jump") && (_isGrounded || Time.time - _lastTimeGrounded <= rememberGroundFor || _additionalJumps > 0) && !_buttonDown)
        {
            _buttonDown = true;
            if (isOnWall)
            {
                _kabullBody.velocity = new Vector2(-_kabullBody.velocity.x * 4, jumpForce * 2);
                _wallAvaliable = false;
                StartCoroutine(WallCooldown(wallCooldownTime));
            }
            else
            {
                _kabullBody.velocity = new Vector2(_kabullBody.velocity.x, jumpForce);
            }
            _additionalJumps--;
        }
    }

    void BettererJump()
    {
        if (_kabullBody.velocity.y < 0)
        {
            _kabullBody.velocity += Vector2.up * Physics2D.gravity * ((lowJumpMultiplaier - 1) * Time.deltaTime);
        }
        else if (_kabullBody.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            _kabullBody.velocity += Vector2.up * Physics2D.gravity * ((lowJumpMultiplaier - 1) * Time.deltaTime);
        }   
    }

    void CheckIfGrounded() 
    {
        Collider2D collidersLeft = Physics2D.OverlapCircle(isGroundedCheckerLeft.position, checkGroundRadius, groundLayer);
        Collider2D collidersRight = Physics2D.OverlapCircle(isGroundedCheckerRight.position, checkGroundRadius, groundLayer);
        
        if (collidersLeft != null || collidersRight != null) 
        {
            _isGrounded = true;
            _additionalJumps = defaultAdditionalJumps;
        }
        else 
        {
            if (_isGrounded) 
            {
                _lastTimeGrounded = Time.time;
            }
            _isGrounded = false;
        }
    }

    private IEnumerator WallCooldown(int time)
    {
        yield return new WaitForSeconds(time);
        _wallAvaliable = true;
    }
    
    void CheckIfOnWall()
    {       
        Collider2D wallCollider= Physics2D.OverlapCircle(wallCheck.position, checkWallRadius, groundLayer);
        
        if (wallCollider != null && _wallAvaliable) 
        {
            _additionalJumps = defaultAdditionalJumps;
            isOnWall = true;
        }
        else
        {
            isOnWall = false;
        }
    }
    
    void Crouch()
    {
        if (Input.GetButton("Crouch"))
        {
            _isCrouched = true;
            standingCollider.enabled = false;
            crouchingCollider.enabled = true;
        }
        else
        {
            _isCrouched = false;
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;
        }
    }
}
