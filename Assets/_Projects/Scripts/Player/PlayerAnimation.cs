using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private InputManager inputManager;
    private Animator animator;

    // Parameters to Optimize Animation
    private int isWalkingHash;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        animator = GetComponentInChildren<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        AnimationHandler();
    }

    void AnimationHandler()
    {
        Vector2 moveInput = inputManager.GetMoveInput();
        bool isMovePressed = moveInput.x != 0 || moveInput.y != 0;

        // Get parameters from Animator Controller
        bool isWalking = animator.GetBool(isWalkingHash);

        if (isMovePressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);    
        } else if (!isMovePressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }

        PlayerInteraction player = GetComponent<PlayerInteraction>();

        if (player.HasPetObject())
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}
