using System.Collections;
using UnityEngine;

public interface ICounterServices
{
    public void ServiceStarting();

    public IEnumerator ServiceOnProgress();

    public void ServiceFinished();

    public void PetRegister(PlayerInteraction player);

    public void PetUnregister(PlayerInteraction player);
}
