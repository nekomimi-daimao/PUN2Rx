using System;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PUN2Rx
{
    public static class InRoomCallbacksTriggersExtension
    {
        public static IObservable<Player> OnPlayerEnteredRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Player>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerEnteredRoomAsObservable();
        }

        public static IObservable<Player> OnPlayerLeftRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Player>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerLeftRoomAsObservable();
        }

        public static IObservable<Hashtable> OnRoomPropertiesUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Hashtable>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnRoomPropertiesUpdateAsObservable();
        }

        public static IObservable<Tuple<Player, Hashtable>> OnPlayerPropertiesUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Tuple<Player, Hashtable>>()
                : GetOrAddComponent<InRoomCallbacksTriggers>(component.gameObject).OnPlayerPropertiesUpdateAsObservable();
        }

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