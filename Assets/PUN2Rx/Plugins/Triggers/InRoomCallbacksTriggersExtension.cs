using System;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PUN2Rx
{
    /// <summary>
    /// <see cref="Photon.Realtime.IInRoomCallbacks"/>
    /// </summary>
    public static class InRoomCallbacksTriggersExtension
    {
        /// <summary>
        /// <c>OnNext(Player)</c> - IInRoomCallbacks.OnPlayerEnteredRoom
        /// </summary>
        public static IObservable<Player> OnPlayerEnteredRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Player>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerEnteredRoomAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Player)</c> - IInRoomCallbacks.OnPlayerLeftRoom
        /// </summary>
        public static IObservable<Player> OnPlayerLeftRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Player>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerLeftRoomAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Hashtable)</c> - IInRoomCallbacks.OnRoomPropertiesUpdate
        /// </summary>
        public static IObservable<Hashtable> OnRoomPropertiesUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Hashtable>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnRoomPropertiesUpdateAsObservable();
        }


        /// <summary>
        /// <c>OnNext(Tuple&lt;PLayer, Hashtable&gt;)</c> - IInRoomCallbacks.OnPlayerPropertiesUpdate
        /// </summary>        
        public static IObservable<Tuple<Player, Hashtable>> OnPlayerPropertiesUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Tuple<Player, Hashtable>>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerPropertiesUpdateAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Player)</c> - IInRoomCallbacks.OnMasterClientSwitched
        /// </summary>
        public static IObservable<Player> OnMasterClientSwitchedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Player>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).onMasterClientSwitchedAsObservable();
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
