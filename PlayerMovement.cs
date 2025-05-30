using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isFacingRight = true;
    public Transform checkPoint;
    Rigidbody2D rb;
    public LayerMask ground;
    public Transform groundCheck;
    public bool canDash = true;
    public bool isDashing;
    private float dashingPower = 12f;
    private float dashingTime = 0.2f;
    public bool grounded = false;
    public Vector3 checkPointPos;
    public float dashNumber = 1f;
    [SerializeField] private TrailRenderer tr;
    public Camera cam;
    
    // State Pattern implementation
    private IPlayerState currentState;
    private NormalState normalState;
    private DashingState dashingState;
    
    // Start is called before the first frame update
    void Start()
    {
        checkPointPos = new Vector3(checkPoint.transform.position.x,checkPoint.transform.position.y,checkPoint.transform.position.z);
        transform.position = new Vector3(checkPoint.transform.position.x,checkPoint.transform.position.y,checkPoint.transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        
        // Initialize states
        normalState = new NormalState();
        dashingState = new DashingState();
        currentState = normalState;
    }

    // Update is called once per frame
    void Update()
    {
        // State management - preserves original isDashing logic
        if (isDashing && currentState != dashingState)
        {
            currentState = dashingState;
        }
        else if (!isDashing && currentState != normalState)
        {
            currentState = normalState;
        }
        
        // Use state pattern for input handling - replaces original Update logic
        currentState.HandleInput(this);
        
        // Original ground check logic preserved
        if (!Physics2D.OverlapCircle(groundCheck.transform.position,0.3f,ground)){
            canDash = true;
            grounded = false;
        }
        if (Physics2D.OverlapCircle(groundCheck.transform.position,0.3f,ground)){
            canDash = false;
            grounded = true;
            dashNumber = 1f;
        }
    }
    
    void FixedUpdate(){
        if (isDashing){
            return;
        }
    }
    
    // Keep original methods for backward compatibility but they're now called through states
    void Move(){
        transform.position = new Vector3(transform.position.x + HorizontalInput() * moveSpeed * Time.deltaTime,transform.position.y,transform.position.z);
    }
    void Jump(){
        if (Input.GetButtonDown("Jump") && grounded){
            SoundManager.PlaySound("JumpSound");
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
        }
    }
    public float HorizontalInput(){
        return Input.GetAxis("Horizontal");
    }

    // Made public for state access and renamed for clarity
    public IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        canDash = true;
    }
    
    // Keep original Dash method for any external references
    private IEnumerator Dash()
    {
        return DashCoroutine();
    }
    
    private void Flip()
    {
        if (isFacingRight && HorizontalInput() < 0f || !isFacingRight && HorizontalInput() > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
