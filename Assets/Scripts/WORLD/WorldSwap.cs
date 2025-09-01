using System.Collections.Generic;
using UnityEngine;

public class WorldSwap : MonoBehaviour
{
    [Header("INPUTS")]
    [SerializeField] private List<KeyCode> swapKeys;

    private PlayerSpriteManager[] playerSpriteManager;
    private SpriteManagerSimple[] spritesManagerSimple;
    private SpriteManager[] spritesManager;
    private bool hasSwap;
    
    private void Update(){
        foreach (KeyCode key in swapKeys){
            if (Input.GetKeyDown(key)){
                applySwap();
                break;
            }
        }
    }

    public bool getHasSwap(){ return hasSwap; }

    private void applySwap(){
        spritesManager = GetComponentsInChildren<SpriteManager>();
        spritesManagerSimple = GetComponentsInChildren<SpriteManagerSimple>();
        playerSpriteManager = GetComponentsInChildren<PlayerSpriteManager>();

        hasSwap = !hasSwap;
        foreach (PlayerSpriteManager psm in playerSpriteManager){
            psm.applySwap();
        }
        
        foreach (SpriteManager sr in spritesManager){
            sr.applySwap();
        }

        foreach (SpriteManagerSimple srs in spritesManagerSimple){
            srs.applySwap();
        }
    }
}
