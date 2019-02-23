using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    /// <summary>
    /// <see cref="Photon.Pun.IPunOwnershipCallbacks"/>
    /// </summary>
    public static class PunOwnershipCallbacksTriggersExtension
    {
        /// <summary>
        /// <c>OnNext(Tuple&lt;PhotonView, Player&gt;)</c> - PunOwnershipCallbacks.OnOwnershipRequest
        /// </summary>
        public static IObservable<Tuple<PhotonView, Player>> OnOwnershipRequestAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Tuple<PhotonView, Player>>()
                : GetOrAddComponent<PunOwnershipCallbacksTriggers>(component.gameObject).OnOwnershipRequestAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Tuple&lt;PhotonView, Player&gt;)</c> - PunOwnershipCallbacks.OnOwnershipTransferred
        /// </summary>
        public static IObservable<Tuple<PhotonView, Player>> OnOwnershipTransferredAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Tuple<PhotonView, Player>>()
                : GetOrAddComponent<PunOwnershipCallbacksTriggers>(component.gameObject).OnOwnershipTransferredAsObservable();
        }

        private static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}