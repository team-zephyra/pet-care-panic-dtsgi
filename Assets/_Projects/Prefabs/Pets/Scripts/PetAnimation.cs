using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PetAnimation : MonoBehaviour
{
    private Animator animator;

    private int isAngryHash;
    private int isGloomyHash;
    private int isHappyHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isAngryHash = Animator.StringToHash("isAngry");
        isGloomyHash = Animator.StringToHash("isGloomy");
        isHappyHash = Animator.StringToHash("isHappy");    
    }

    void Update()
    {
        // Testing Trigger Animation

        if (Keyboard.current.numpad1Key.isPressed)
        {
            TriggerAngryAnimation();
        }

        if (Keyboard.current.numpad2Key.isPressed)
        {
            TriggerGloomyAnimation();
        }

        if (Keyboard.current.numpad3Key.isPressed)
        {
            TriggerHappyAnimation();
        }
        
    }

    public void TriggerAngryAnimation()
    {
        // To-Do:
        // Tie in pet mood/happiness value to trigger animation

        animator.SetTrigger(isAngryHash);
    }

    public void TriggerGloomyAnimation()
    {
        // To-Do:
        // Tie in pet mood/happiness value to trigger animation

        animator.SetTrigger(isGloomyHash);
    }

    public void TriggerHappyAnimation()
    {
        // To-Do:
        // Tie in pet mood/happiness value to trigger animation

        animator.SetTrigger(isHappyHash);
    }
}
