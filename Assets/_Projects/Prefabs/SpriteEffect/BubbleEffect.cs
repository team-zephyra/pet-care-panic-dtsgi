using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class BubbleEffect : MonoBehaviour
{
    [Header("Setup Construct")]
    [SerializeField] private SpriteRenderer bubbleObject;
    [SerializeField] private SpriteRenderer bubbleRenderer;
    private Animator anim;

    [Header("Properties")]
    [SerializeField] private Sprite objectBubleImage;
    [SerializeField] private Sprite finishedImage;

    private void Awake()
    {
        if(bubbleObject== null)
        {
            bubbleObject = GetComponent<SpriteRenderer>();
        }
        if (bubbleRenderer == null)
        {
            bubbleRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        bubbleRenderer.enabled = false;
        bubbleObject.enabled = false;
    }

    public void SetBubbleImage(Sprite _image)
    {
        objectBubleImage = _image;
    }

    public void Enable(BubbleType _type)
    {
        switch (_type)
        {
            case BubbleType.objectBuble: bubbleRenderer.sprite = objectBubleImage; break;
            case BubbleType.finish:  bubbleRenderer.sprite = finishedImage; break;
        }

        PopUpBubble();
    }

    public void Disable()
    {
        CloseBubble();
    }

    void CloseBubble()
    {
        anim.SetTrigger("popout");
    }

    void PopUpBubble()
    {
        anim.SetTrigger("popup");
    }
}

public enum BubbleType
{
    objectBuble,
    finish
}
