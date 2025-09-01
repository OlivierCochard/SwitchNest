using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private PlayerManager playerManager;

    private void OnTriggerEnter2D(Collider2D col){
        //WIN
        if (col.tag == "Finish"){
            playerManager.applyWin();
            return;
        }

        //DEATH
        if (col.tag == "Spike"){
            playerManager.applyLose();
            return;
        }
        if (col.tag == "Out"){
            playerManager.applyLose();
            return;
        }
        if (col.tag == "Crush"){
            playerManager.applyLose();
            return;
        }

        //SPECIAL
        if (col.tag == "Trampoline"){
            playerManager.applyTrampoline(col.transform.up);

            SpriteManager spriteManagerCol = col.transform.GetComponent<SpriteManager>();
            if (spriteManagerCol != null)
                spriteManagerCol.applyAnimation();
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D col){
        //DEATH
        if (col.gameObject.tag == "Monster"){
            playerManager.applyLose();
            return;
        }

        //SPECIAL
        if (col.gameObject.tag == "Bouncer"){
            float collisionForce = col.relativeVelocity.magnitude;

            if (collisionForce > 5){
                SpriteManager spriteManagerCol = col.transform.GetComponent<SpriteManager>();
                if (spriteManagerCol != null)
                    spriteManagerCol.applyAnimation();
                return;
            }
            playerManager.resetVelocityY();
            return;
        }
    }
}
