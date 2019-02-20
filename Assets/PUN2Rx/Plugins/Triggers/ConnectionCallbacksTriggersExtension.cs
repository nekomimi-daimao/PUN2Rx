using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    public static class ConnectionCallbacksTriggersExtension
    {
        public static IObservable<Unit> OnConnectedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnConnectedAsObservable();
        }

        public static IObservable<Unit> OnConnectedToMasterAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnConnectedToMasterAsObservable();
        }

        public static IObservable<DisconnectCause> OnDisconnectedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<DisconnectCause>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnDisconnectedAsObservable();
        }

        public static IObservable<RegionHandler> OnRegionListReceivedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<RegionHandler>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnRegionListReceivedAsObservable();
        }

        public static IObservable<Dictionary<string, object>> OnCustomAuthenticationResponseAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Dictionary<string, object>>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnCustomAuthenticationResponseAsObservable();
        }

        public static IObservable<string> OnCustomAuthenticationFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<string>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnCustomAuthenticationFailedAsObservable();
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