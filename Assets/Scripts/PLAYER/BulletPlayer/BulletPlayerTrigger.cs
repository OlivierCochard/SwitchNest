using UnityEngine;

public class BulletPlayerTrigger : MonoBehaviour
{
    [Header("PARAMETERS")]
    [SerializeField] private int timeBeforeDestroy;
    [SerializeField] private LayerMask layerToIgnore;

    private void Start(){
        Collider2D bulletCollider = GetComponent<Collider2D>();
        if (bulletCollider != null){
            foreach (Collider2D collider in FindObjectsOfType<Collider2D>()){
                if (((1 << collider.gameObject.layer) & layerToIgnore) != 0){
                    Physics2D.IgnoreCollision(bulletCollider, collider);
                }
            }
        }

        Destroy(gameObject, timeBeforeDestroy);
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (((1 << col.gameObject.layer) & layerToIgnore) != 0){
            return;
        }

        if (col.gameObject.tag == "Monster"){
            Destroy(col.transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
}
