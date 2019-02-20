using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    public static class LobbyCallbacksTriggersExtension
    {
        public static IObservable<Unit> OnJoinedLobbyAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnJoinedLobbyAsObservable();
        }

        public static IObservable<Unit> OnLeftLobbyAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnLeftRoomAsObservable();
        }

        public static IObservable<List<RoomInfo>> OnRoomListUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<List<RoomInfo>>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnRoomListUpdateAsObservable();
        }

        public static IObservable<List<TypedLobbyInfo>> OnLobbyStatisticsUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<List<TypedLobbyInfo>>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnLobbyStatisticsUpdateAsObservable();
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
