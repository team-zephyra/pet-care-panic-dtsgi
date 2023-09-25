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
        //NewOrder(PetCategory.Cat_Calico, OrderType.Bathing);
        //NewOrder(PetCategory.Cat_Oren, OrderType.Grooming);
        //NewOrder(PetCategory.Cat_Siamese, OrderType.Grooming);
        //NewOrder(PetCategory.Dog_Rottweiler, OrderType.Daycare);
    }

    public OrderCard NewOrder(PetCategory _pet, OrderType _order, int _orderIndex)
    {
        OrderCard oCard = Instantiate(prefCard, panelOrderParent);

        // Get Pet Object from GameManager
        PetObjectSO petObject;
        petObject = GameManager.instance.petInitiate.GetPetsByCategory(_pet);

        // Create OrderCardObject for setup Card
        OrderCardObjectSO[] orderCards = null;

        switch (_order)
        {
            case OrderType.Bathing: orderCards = needsBathing; break;
            case OrderType.Grooming: orderCards = needsGrooming; break;
            case OrderType.Daycare: orderCards = needsDaycare; break;
        }

        oCard.SetupCard(orderCards);
        oCard.SetIconCard(petObject.petIcon);
        oCard.orderCardIndex = _orderIndex;

        oCard.gameObject.SetActive(true);

        return oCard;
    }

    public void OrderQueue()
    {

    }
}
