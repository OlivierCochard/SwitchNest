using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [Header("SPRITES")]
    [SerializeField] private Sprite[] sprite_main;
    [SerializeField] private Sprite[] sprite_outline;
    [SerializeField] private Sprite[] sprite_animation_main;
    [SerializeField] private Sprite[] sprite_animation_outline;

    [Header("PARAMETERS")]
    [SerializeField] private bool otherDimension;
    [SerializeField] private float timeBetweenFrame;
    [SerializeField] private float timeBetweenAnimation;

    private Collider2D[] listCollider2D;
    private SpriteRenderer sr;
    private int initialOrder;
    private int initialLayer;
    private int currentIndexSprite;
    private bool switched;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialLayer = gameObject.layer;
        initialOrder = sr.sortingOrder;

        if (sprite_main.Length > 1)
            InvokeRepeating("nextFrame", timeBetweenFrame, timeBetweenFrame);

        sr.sprite = otherDimension ? sprite_outline[0] : sprite_main[0];
        switched = otherDimension;
        sr.sortingOrder = calculateSortingOrder();

        listCollider2D = GetComponents<Collider2D>();
        updateColliders();
    }

    private void nextFrame(){
        currentIndexSprite ++;
        if (currentIndexSprite >= sprite_main.Length){
            currentIndexSprite = 0;
        }
        sr.sprite = switched ? sprite_outline[currentIndexSprite] : sprite_main[currentIndexSprite];
    }
    private void nextAnimation(){
        currentIndexSprite ++;
        if (currentIndexSprite >= sprite_animation_main.Length){
            if (switched){
                sr.sprite = sprite_outline[0];
            }
            else{
                sr.sprite = sprite_main[0];
            }
            currentIndexSprite = 0;

            if (sprite_main.Length > 1)
                InvokeRepeating("nextFrame", timeBetweenFrame, timeBetweenFrame);
            return;
        }

        if (switched){
            sr.sprite = sprite_animation_outline[currentIndexSprite];
        }
        else{
            sr.sprite = sprite_animation_main[currentIndexSprite];
        }

        Invoke("nextAnimation", timeBetweenAnimation);
    }

    public void applySwap(){
        switched = !switched;

        if (currentIndexSprite >= sprite_main.Length){
            CancelInvoke("nextAnimation");
            currentIndexSprite = 0;
        }

        if (switched){
            sr.sprite = sprite_outline[currentIndexSprite];
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else{
            sr.sprite = sprite_main[currentIndexSprite];
            gameObject.layer = initialLayer;
        }

        sr.sortingOrder = calculateSortingOrder();
        updateColliders();
    }
    public void applyAnimation(){
        if (sprite_animation_main.Length == 0){
            return;
        }
        CancelInvoke("nextFrame");
        CancelInvoke("nextAnimation");
        currentIndexSprite = 0;

        if (switched){
            sr.sprite = sprite_animation_outline[0];
        }
        else{
            sr.sprite = sprite_animation_main[0];
        }

        Invoke("nextAnimation", timeBetweenAnimation);
    }

    private void updateColliders(){
        if (listCollider2D != null && listCollider2D.Length > 0){
            foreach (Collider2D col in listCollider2D){
                col.enabled = !switched;
            }
        }
    }

    private int calculateSortingOrder(){
        return !switched ? initialOrder : initialOrder - 1;
    }
}
