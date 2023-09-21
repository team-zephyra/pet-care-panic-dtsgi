using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orders : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform panelOrderParent;

    [Header("Properties")]
    [SerializeField] private OrderCard prefCard;
    [SerializeField] private OrderCardObjectSO[] needsBathing;
    [SerializeField] private OrderCardObjectSO[] needsGrooming;
    [SerializeField] private OrderCardObjectSO[] needsDaycare;

    private void Start()
    {
        NewOrder(PetType.Cat, OrderType.Bathing);
        NewOrder(PetType.Cat, OrderType.Grooming);
        NewOrder(PetType.Cat, OrderType.Daycare);
    }

    public void NewOrder(PetType _pet, OrderType _order)
    {
        OrderCard oCard = Instantiate(prefCard, panelOrderParent);

        switch (_order)
        {
            case OrderType.Bathing: oCard.NewOrderCard(needsBathing); break;
            case OrderType.Grooming: oCard.NewOrderCard(needsGrooming); break;
            case OrderType.Daycare: oCard.NewOrderCard(needsDaycare); break;
        }

        oCard.gameObject.SetActive(true);
        
    }

    public void OrderQueue()
    {

    }
}
