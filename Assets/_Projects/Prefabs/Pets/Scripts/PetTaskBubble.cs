using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetTaskBubble : MonoBehaviour
{
    private Camera mainCamera;
    private Pet pet;
    private Image bubbleImage;

    public Sprite needBathingSprite;
    public Sprite needDryingSprite;
    public Sprite serviceIsDone;
    

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePetBubbleRotation();
        UpdatePetBubbleSprite();
    }

    private void Setup()
    {
        pet = GetComponentInParent<Pet>();
        bubbleImage = GetComponent<Image>();
        mainCamera = Camera.main;
    }

    private void UpdatePetBubbleSprite()
    {
        if (!pet.isBathingDone && !pet.isDryingDone)
        {
            bubbleImage.sprite = needBathingSprite;
        }
        else if (pet.isBathingDone && !pet.isDryingDone)
        {
            bubbleImage.sprite = needDryingSprite;
        }
        else if (pet.isBathingDone && pet.isDryingDone)
        {
            bubbleImage.sprite = serviceIsDone;
        }
    }

    private void UpdatePetBubbleRotation()
    {
        transform.rotation = mainCamera.transform.rotation;
    }

    public void EnableBubbleImage()
    {
        bubbleImage.enabled = true;
    }

    public void DisableBubbleImage()
    {
        bubbleImage.enabled = false;
    }
}
