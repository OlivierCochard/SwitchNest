using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{   
    [Header("CLASS")]
    [SerializeField] private PlayerSpriteManager playerSpriteManager;

    [Header("TOOLS")]
    [SerializeField] private Rigidbody2D rb;

    [Header("PARAMETERS")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDoubleForce;
    [SerializeField] private float jumpTrampolineForce;
    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform aroundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float aroundCheckRadius;

    [Header("INPUTS")]
    [SerializeField] private List<KeyCode> jumpKeys;

    

    private void Update(){
        foreach (KeyCode key in jumpKeys){
            if (Input.GetKeyDown(key)){
                checkJump();
                break;
            }
        }
    }

    private void checkJump(){
        bool isArounded = Physics2D.OverlapCircle(groundCheck.position, aroundCheckRadius, jumpLayer);
        if (isArounded){
            applyJump(jumpDoubleForce);
            return;
        }


        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, blockLayer);
        if (isGrounded == true){
            applyJump(jumpForce);
        }
    }
    private void applyJump(float jumpForce, Vector2? direction = null){
        if (rb == null) return;

        float velocityX = 0;
        if (direction == null){
            direction = Vector2.up;
            velocityX = rb.velocity.x;
        }

        rb.velocity = new Vector2(velocityX, 0);
        rb.AddForce((Vector2) direction * jumpForce, ForceMode2D.Impulse);
    }

    public void applyTrampolineJump(Vector2 direction){
        applyJump(jumpTrampolineForce, direction);
    }
    public void resetVelocityY(){
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

}
