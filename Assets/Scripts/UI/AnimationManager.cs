using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void applyEndGame(){
        animator.SetBool("endGame", true);
    }
}
