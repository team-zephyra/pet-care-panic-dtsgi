using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimation : MonoBehaviour
{
    [Header("Required References")]
    private Pet pet;
    private Animator animator;

    [Header("Animation Settings")]
    private int isAngryHash;
    private int isGloomyHash;
    private int isHappyHash;
    private int isSadHash;

    // Start is called before the first frame update
    void Start()
    {
        pet = GetComponent<Pet>();
        animator = GetComponent<Animator>();

        isAngryHash = Animator.StringToHash("isAngry");
        isGloomyHash = Animator.StringToHash("isGloomy");
        isHappyHash = Animator.StringToHash("isHappy");
        isSadHash = Animator.StringToHash("isSadHash");
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
        animator.SetTrigger(isHappyHash);
    }

    public void TriggerSadAnimation()
    {
        animator.SetTrigger(isSadHash);
    }
}
