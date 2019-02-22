using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    public static class PunOwnershipCallbacksTriggersExtension
    {
        public static IObservable<Tuple<PhotonView, Player>> OnOwnershipRequestAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Tuple<PhotonView, Player>>()
                : GetOrAddComponent<PunOwnershipCallbacksTriggers>(component.gameObject).OnOwnershipRequestAsObservable();
        }

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