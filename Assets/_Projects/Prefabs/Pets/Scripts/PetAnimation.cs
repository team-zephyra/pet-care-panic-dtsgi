using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PetAnimation : MonoBehaviour
{
    private Pet pet;
    private Animator animator;

    private int isAngryHash;
    private int isGloomyHash;
    private int isHappyHash;

    // Start is called before the first frame update
    void Start()
    {
        pet =GetComponent<Pet>();
        animator = GetComponent<Animator>();

        isAngryHash = Animator.StringToHash("isAngry");
        isGloomyHash = Animator.StringToHash("isGloomy");
        isHappyHash = Animator.StringToHash("isHappy");
    }

    void Update()
    {
        PlayGloomyAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Counter_Checkin>() != null)
        {
            pet.isOnCheckInCounter = true;
        }
        else if (other.GetComponent<Counter_Checkin>() == null)
        {
            pet.isOnCheckInCounter = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Counter_Checkin>() != null)
        {
            pet.isOnCheckInCounter = true;
        }
        else if (other.GetComponent<Counter_Checkin>() == null)
        {
            pet.isOnCheckInCounter = false;
        }
    }

    public void TriggerAngryAnimation()
    {
        // To-Do:
        // Tie in pet mood/happiness value to trigger animation

        animator.SetTrigger(isAngryHash);
    }

    public void PlayGloomyAnimation()
    {
        // Play Gloomy animation if Pet is in Check In Counter
        if (pet.isOnCheckInCounter)
        {
            animator.SetBool(isGloomyHash, true);
        } else
        {
            animator.SetBool(isGloomyHash, false);
        }
        
    }

    public void TriggerHappyAnimation()
    {
        // To-Do:
        // Tie in pet mood/happiness value to trigger animation

        animator.SetTrigger(isHappyHash);
    }
}
