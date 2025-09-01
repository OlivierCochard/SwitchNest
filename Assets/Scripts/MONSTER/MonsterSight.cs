using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    [Header("TARGET")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask obstacleLayer;

    private bool isPlayerInSight;


    private void Update(){
        UpdatePlayerVisibility();
    }
    private void UpdatePlayerVisibility(){
        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Block")) {
            isPlayerInSight = false;
        } else {
            isPlayerInSight = true;
        }
    }


    public bool getIsPlayerInSight(){ return isPlayerInSight; }
}
