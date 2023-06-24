using System.Threading.Tasks;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Events;

public class ApcsNetworkBehaviour : NetworkBehaviour
{
    protected async void IfIsOwnerThenDo(UnityAction ownerCallback)
    {
        var waitTime = 3f;
        const float checkTime = 0.5f;
        while (!base.IsOwner && waitTime > 0f)
        {
            waitTime -= checkTime;
            await Task.Delay((int)(checkTime * 1000));
        }

        if (base.IsOwner)
        {
            ownerCallback.Invoke();
        }
        else
        {
            Debug.LogError(base.IsOwner);
        }
    }
}