using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    /// <summary>
    /// <see cref="Photon.Realtime.IConnectionCallbacks"/>
    /// </summary>
    public static class ConnectionCallbacksTriggersExtension
    {
        /// <summary>
        /// <c>OnNext(Unit)</c> - IConnectionCallbacks.OnConnected
        /// </summary>
        public static IObservable<Unit> OnConnectedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnConnectedAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Unit)</c> - IConnectionCallbacks.OnConnectedToMaster
        /// </summary>
        public static IObservable<Unit> OnConnectedToMasterAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnConnectedToMasterAsObservable();
        }

        /// <summary>
        /// <c>OnNext(DisconnectCause)</c> - IConnectionCallbacks.OnDisconnected
        /// </summary>
        public static IObservable<DisconnectCause> OnDisconnectedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<DisconnectCause>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnDisconnectedAsObservable();
        }

        /// <summary>
        /// <c>OnNext(RegionHandler)</c> - IConnectionCallbacks.OnRegionListReceived
        /// </summary>
        public static IObservable<RegionHandler> OnRegionListReceivedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<RegionHandler>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnRegionListReceivedAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Tuple&lt;string, object&gt;)</c> - IConnectionCallbacks.OnCustomAuthenticationResponse
        /// </summary>
        public static IObservable<Dictionary<string, object>> OnCustomAuthenticationResponseAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Dictionary<string, object>>()
                : GetOrAddComponent<ConnectionCallbacksTriggers>(component.gameObject).OnCustomAuthenticationResponseAsObservable();
        }

        /// <summary>
        /// <c>OnError()</c> - IConnectionCallbacks.OnCustomAuthenticationFailed
        /// </summary>
        public static IObservable<Unit> OnCustomAuthenticationFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
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