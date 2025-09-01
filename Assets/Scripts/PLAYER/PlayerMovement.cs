using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private PlayerSpriteManager playerSpriteManager;

    [Header("TOOLS")]
    [SerializeField] private Rigidbody2D rb;
    
    [Header("PARAMETERS")]
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private float oppositeNewSpeed;
    [SerializeField] private float maxSpeed;

    [Header("INPUTS")]
    [SerializeField] private List<KeyCode> leftKeys;
    [SerializeField] private List<KeyCode> rightKeys;

    private int lastDirection;
    private bool isRightKey;
    private bool isLeftKey;


    private void Update(){
        if (rb == null) return;

        isLeftKey = false;
        foreach (KeyCode key in leftKeys){
            if (Input.GetKey(key)){
                isLeftKey = true;
                break;
            }
        }

        isRightKey = false;
        foreach (KeyCode key in rightKeys){
            if (Input.GetKey(key)){
                isRightKey = true;
                break;
            }
        }
    }
    private void FixedUpdate(){
        if (rb == null) return;

        int direction = calculInputDirection(isLeftKey, isRightKey);
        playerSpriteManager.flip(direction);
        applyMovement(direction);
    }

    private int calculInputDirection(bool isLeftKey, bool isRightKey){
        int res = 0;
        if (isRightKey) res++;
        if (isLeftKey) res--;
        return res;
    }
    private void applyMovement(int direction){
        float res = 0;
        if (direction == 0){
            if (rb.velocity.x > 0){
                res = rb.velocity.x - decelerationSpeed;
                if (res < 0) res = 0;
            }
            if (rb.velocity.x < 0){
                res = rb.velocity.x + decelerationSpeed;
                if (res > 0) res = 0;
            }
        }
        else {
            //NEW SPEED
            if (direction == lastDirection){
                if (direction == -1){
                    res = rb.velocity.x - accelerationSpeed;
                    if (rb.velocity.x < -maxSpeed){
                        res = rb.velocity.x + accelerationSpeed;
                        if (res > -maxSpeed){
                            res = -maxSpeed;
                        }
                    }
                    else if (res < -maxSpeed){
                        res = -maxSpeed;
                    }
                }
                else if (direction == 1){
                    res = rb.velocity.x + accelerationSpeed;
                    if (rb.velocity.x > maxSpeed){
                        res = rb.velocity.x - accelerationSpeed;
                        if (res < maxSpeed){
                            res = maxSpeed;
                        }
                    }
                    else if (res > maxSpeed){
                        res = maxSpeed;
                    }
                }
            }
            else {
                float newOppositeNewSpeed = oppositeNewSpeed;
                if (Mathf.Abs(rb.velocity.x) < oppositeNewSpeed){
                    newOppositeNewSpeed = Mathf.Abs(rb.velocity.x);
                }
                res = direction * newOppositeNewSpeed;

                if (Mathf.Abs(rb.velocity.x) > maxSpeed){
                    if (rb.velocity.x > 0){
                        res = rb.velocity.x - decelerationSpeed;
                        if (res < 0) res = 0;
                    }
                    if (rb.velocity.x < 0){
                        res = rb.velocity.x + decelerationSpeed;
                        if (res > 0) res = 0;
                    }
                }
            }
        }
        rb.velocity = new Vector2(res, rb.velocity.y);
        lastDirection = direction;
    }
}
