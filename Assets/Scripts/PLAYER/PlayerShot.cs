using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("CLASS")]
    [SerializeField] private WorldSwap worldSwap;

    [Header("TOOLS")]
    [SerializeField] private GameObject bulletPlayerPrefab;

    [Header("PARAMETERS")]
    [SerializeField] private float shotForce;

    [Header("SPRITES")]
    [SerializeField] private Sprite bulletSprite_1;
    [SerializeField] private Sprite bulletSprite_2;


    public void shot(Vector2 pos){
        GameObject obj = Instantiate(bulletPlayerPrefab);
        obj.transform.position = pos;

        /*
        Sprite sprite = null;
        int number = worldSwap.getNumberWorld();
        if (number == 1){
            sprite = bulletSprite_1;
        }
        else if (number == 2){
            sprite = bulletSprite_2;
        }
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
        */

        obj.GetComponent<Rigidbody2D>().AddForce(Vector2.down * shotForce, ForceMode2D.Impulse);

    }
}
