using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleEffect : MonoBehaviour
{
    [Header("Setup Construct")]
    private SpriteRenderer bubbleRenderer;
    private Animator anim;

    [Header("Properties")]
    [SerializeField] private Sprite counterImage;
    [SerializeField] private Sprite finishedImage;

    private void Awake()
    {
        if (bubbleRenderer == null)
        {
            bubbleRenderer = GetComponent<SpriteRenderer>();
        }
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }

        bubbleRenderer.enabled = false;
    }

    public void Enable(BubbleType _type)
    {
        switch (_type)
        {
            case BubbleType.counter: bubbleRenderer.sprite = counterImage; break;
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
    counter,
    finish
}
