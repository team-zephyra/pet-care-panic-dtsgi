using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet : MonoBehaviour
{
    /* Pet Scriptable Object */
    private PetObjectSO petObjectSO;

    [Header("Pet Expression")]
    [SerializeField] private BubbleEffect currentBubbleEffect;
    [SerializeField] private BubbleEffectSO[] bubbleOrderSO;
    [SerializeField] private BubbleEffectSO bubblePetSO;
    private BubbleEffectSO currentBubbleSO;
    

    /* Pet & Counters */
    [Header("Pet Dep Counters")]
    private IPetObjectParent petObjectParent;
    public bool isOnCheckInCounter;

    /* Pet Order */
    [Header("Pet Order")]
    public int pet_order_index;
    private OrderCardObjectSO[] needs;
    private OrderType orderType;
    [SerializeField]private int taskCompleted = 0;

    /* Pet Happiness */
    [Header("Happiness Settings")]
    [SerializeField] private PetHappinessBar happinessBar;
    private float decreaseHappinessStartDelay = 6f;
    private float decreaseHappinessRate = 6f;
    private float currentHappiness;
    private float maxHappiness = 3;

    private void Start()
    {
        happinessBar = GetComponentInChildren<PetHappinessBar>();

        currentHappiness = maxHappiness;
        //happinessBar.UpdateHappinessBar(currentHappiness, maxHappiness);
    }

    private void Update()
    {
        // happinessBar.UpdateHappinessBar(currentHappiness, maxHappiness); Moved to Start
    }

    #region Pet Order

    public void SetupPetNeeds(OrderCardObjectSO[] _needs, OrderType _ordertype)
    {
        needs = _needs;
        orderType = _ordertype;

        switch(orderType)
        {
            case OrderType.Bathing: currentBubbleSO = bubbleOrderSO[0]; break;
            case OrderType.Grooming: currentBubbleSO = bubbleOrderSO[1]; break;
            case OrderType.Daycare: currentBubbleSO = bubbleOrderSO[2]; break;
        }

        StartCoroutine(ShowBubble(currentBubbleSO.imageSprite[taskCompleted]));
    }

    private IEnumerator ShowBubble(Sprite _image)
    {
        currentBubbleEffect.SetBubbleImage(_image);
        currentBubbleEffect.Enable(BubbleType.objectBuble);
        yield return new WaitForSeconds(2f);
        currentBubbleEffect.Disable();
    }

    public void UpdateTaskComplete(OrderTaskCategory _cat)
    {
        if(taskCompleted >0 && taskCompleted <3)
        {
            if (needs[taskCompleted].sorted)
            {
                int indexTaskBefore = 1;
                if(needs[taskCompleted].taskType == OrderTaskCategory.Drying)
                {
                    indexTaskBefore = 2;
                }

                OrderCardObjectSO taskBefore = needs[taskCompleted - indexTaskBefore];
                if (taskBefore.completed)
                {
                    needs[taskCompleted].completed = true;
                    taskCompleted += 1;
                }

            }
            else
            {
                needs[taskCompleted].completed = true;
                taskCompleted += 1;
            }
        }
        else if(taskCompleted == 0)
        {
            if (needs[taskCompleted].taskType == _cat)
            {
                needs[taskCompleted].completed = true;
                taskCompleted += 1;
                if (needs[taskCompleted].taskType == OrderTaskCategory.Next)
                {
                    taskCompleted+= 1;
                }
            }
        }
        else
        {
            Debug.Log("task Completed");
        }
    }

    public OrderTaskCategory CheckNeedsCategory()
    {
        if(taskCompleted < 3)
        {
            return needs[taskCompleted].taskType;

        }
        else
        {
            return OrderTaskCategory.Next;
        }
    }

    public int TaskCompleted { get => taskCompleted;}
    #endregion

    #region Pet Happiness

    public void PetExpression(EPetExpression _expresion)
    {
        int idx = 0;
        switch (_expresion)
        {
            case EPetExpression.Happy: idx = 0; break;
            case EPetExpression.Angry: idx = 1; break;
            case EPetExpression.Gloomy: idx = 2; break;
            case EPetExpression.Win: idx = 3; break;
        }
        StartCoroutine(ShowBubble(bubblePetSO.imageSprite[idx]));
    }

    private void DecreaseHappiness()
    {
        currentHappiness -= 0.5f;

        //Debug.Log(gameObject.name + " happiness is " + currentHappiness);

        if (currentHappiness <= 0)
        {
            // TO DO
            // Handle pet's state when happiness reaches 0 or below
            StopDecreaseHappiness();
        }

        StartCoroutine(ShowBubble(currentBubbleSO.imageSprite[taskCompleted]));
    }

    public void StartDecreaseHappiness()
    {
        InvokeRepeating("DecreaseHappiness", decreaseHappinessStartDelay, decreaseHappinessRate);
    }

    public void StopDecreaseHappiness()
    {
        CancelInvoke("DecreaseHappiness");
    }

    public PetObjectSO GetPetObjectSO()
    {
        return petObjectSO;
    }

    #endregion

    #region Authority
    public void SetPetObjectParent(IPetObjectParent _petObjectParent)
    {
        if (this.petObjectParent != null)
        {
            this.petObjectParent.ClearPetObject();
        }

        this.petObjectParent = _petObjectParent;

        if (_petObjectParent.HasPetObject())
        {
            Debug.LogError("Counter already has a PetObject!");
        }

        _petObjectParent.SetPetObject(this);

        transform.parent = _petObjectParent.GetSurfacePosition();
        transform.eulerAngles = transform.parent.eulerAngles;
        transform.localPosition = Vector3.zero;
    }

    public IPetObjectParent GetPetObjectParent()
    {
        return petObjectParent;
    }
    #endregion

    #region CheckOuting

    public void CheckoutPet()
    {
        petObjectParent.ClearPetObject();
        Destroy(gameObject);
    }

    #endregion
}
