using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShot : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private MonsterSight monsterSight;
    [SerializeField] private WorldSwap worldSwap;

    [Header("TOOLS")]
    [SerializeField] private GameObject bulletMonsterPrefab;

    [Header("PARAMETERS")]
    [SerializeField] private float shotForce;
    [SerializeField] private float shotDelay;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Collider2D monsterCollider;

    [Header("SPRITES")]
    [SerializeField] private Sprite bulletSprite_1;
    [SerializeField] private Sprite bulletSprite_2;

    private bool isShotReady = true;


    private void Update(){
        if (monsterSight.getIsPlayerInSight()){
            tryShot();
        }
    }

    private void tryShot(){
        if (isShotReady){
            shot();
            isShotReady = false;
            Invoke("setShotReadyTrue", shotDelay);
            return;
        }
    }
    private void shot(){
        GameObject obj = Instantiate(bulletMonsterPrefab);
        obj.transform.position = transform.position;
        obj.transform.parent = transform;
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), monsterCollider, true);

        Sprite sprite = null;
        Sprite otherSprite = null;
        if (worldSwap.getHasSwap()){
            sprite = bulletSprite_2;
            otherSprite = bulletSprite_1;
        }
        else {
            sprite = bulletSprite_1;
            otherSprite = bulletSprite_2;
        }
        obj.GetComponent<SpriteManagerSimple>().sprite_2 = otherSprite;
        obj.GetComponent<SpriteManagerSimple>().sprite_1 = sprite;
        obj.GetComponent<SpriteRenderer>().sprite = sprite;

        if (sprite == bulletSprite_2){
            obj.GetComponent<SpriteManagerSimple>().applySwap();
        }

        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        obj.GetComponent<Rigidbody2D>().AddForce(directionToPlayer * shotForce, ForceMode2D.Impulse);
    }

    private void setShotReadyTrue(){
        isShotReady = true;
    }
}
