using UnityEngine;

public class PlayerSpriteManager : MonoBehaviour
{
    [Header("SPRITES")]
    [SerializeField] private Sprite playerSprite_1;
    [SerializeField] private Sprite playerSprite_2;
    
    private Sprite currentSprite;
    private SpriteRenderer sr;

    
    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
        currentSprite = playerSprite_1;
    }

    public void applySwap(){
        if (currentSprite == playerSprite_1){
            currentSprite = playerSprite_2;
        }
        else if (currentSprite == playerSprite_2){
            currentSprite = playerSprite_1;
        }
        else {
            Debug.Log("error sprite swap"); 
            return;
        }

        sr.sprite = currentSprite;
    }

    public void flip(int direction){
        if (direction == 1){
            sr.flipX = false;
        }
        else if (direction == -1){
            sr.flipX = true;
        }
        else return;
    }
    public void flip(bool boolean){
        sr.flipX = boolean;
    }
}
