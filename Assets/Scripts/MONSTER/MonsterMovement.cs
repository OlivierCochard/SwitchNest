using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private PlayerSpriteManager monsterSpriteManager;
    [SerializeField] private MonsterSight monsterSight;

    [Header("TOOLS")]
    [SerializeField] private Rigidbody2D rb;

    [Header("PARAMETERS")]
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private float oppositeNewSpeed;
    [SerializeField] private float maxSpeed;

    [Header("TARGET")]
    [SerializeField] private Transform playerTransform;

    private int lastDirection;
    private int playerDirection;
    private bool isPlayerInSight;


    private void Update(){
        if (rb == null) return;

        UpdatePlayerDirection();;
    }
    private void UpdatePlayerDirection(){
        if (monsterSight.getIsPlayerInSight() == false){
            playerDirection = 0;
            return;
        }

        float monsterPosX = transform.position.x;
        float playerPosX = playerTransform.position.x;

        float distance = Mathf.Abs(playerPosX - monsterPosX);
        if (transform.position.x < playerPosX){
            playerDirection = 1;
        }
        else if (transform.position.x > playerPosX){
            playerDirection = -1;
        }
    }

    private void FixedUpdate(){
        if (rb == null) return;

        if (playerDirection == -1){
            monsterSpriteManager.flip(true);
        }
        else if (playerDirection == 1){
            monsterSpriteManager.flip(false);
        }
        
        applyMovement(playerDirection);
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
                }
                else if (direction == 1){
                    res = rb.velocity.x + accelerationSpeed;
                }
            }
            else {
                float newOppositeNewSpeed = oppositeNewSpeed;
                if (Mathf.Abs(rb.velocity.x) < oppositeNewSpeed){
                    newOppositeNewSpeed = Mathf.Abs(rb.velocity.x);
                }
                res = direction * newOppositeNewSpeed;
            }

            //SECU MAX SPEED
            if (res > maxSpeed){
                res = maxSpeed;
            }
            else if (res < -maxSpeed){
                res = -maxSpeed;
            }
        }
        rb.velocity = new Vector2(res, rb.velocity.y);
        lastDirection = direction;
    }
}
