using UnityEngine;

public class SpriteManagerSimple : MonoBehaviour
{
    [Header("SPRITES")]
    public Sprite sprite_1;
    public Sprite sprite_2;
    
    private Sprite currentSprite;
    private SpriteRenderer sr;

    
    private void Awake(){
        sr = GetComponent<SpriteRenderer>();
        currentSprite = sprite_1;
    }

    public void applySwap(){
        if (currentSprite == sprite_1){
            currentSprite = sprite_2;
        }
        else if (currentSprite == sprite_2){
            currentSprite = sprite_1;
        }
        else if (currentSprite == null){
            Debug.Log("error sprite null"); 
            return;
        }
        else {
            Debug.Log("error sprite unknown"); 
            return;
        }
        sr.sprite = currentSprite;
    }
}
