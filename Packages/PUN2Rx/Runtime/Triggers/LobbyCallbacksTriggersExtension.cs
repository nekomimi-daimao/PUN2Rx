using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    /// <summary>
    /// <see cref="Photon.Realtime.ILobbyCallbacks"/>
    /// </summary>
    public static class LobbyCallbacksTriggersExtension
    {
        /// <summary>
        /// <c>OnNext(Unit)</c> - ILobbyCallbacks.OnJoinedLobby
        /// </summary>
        public static IObservable<Unit> OnJoinedLobbyAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnJoinedLobbyAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Unit)</c> - ILobbyCallbacks.OnLeftLobby
        /// </summary>
        public static IObservable<Unit> OnLeftLobbyAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnLeftRoomAsObservable();
        }

        /// <summary>
        /// <c>OnNext(List&lt;RoomInfo&gt;)</c> - ILobbyCallbacks.OnRoomListUpdate
        /// </summary>        
        public static IObservable<List<RoomInfo>> OnRoomListUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<List<RoomInfo>>()
                : GetOrAddComponent<LobbyCallbacksTriggers>(component.gameObject).OnRoomListUpdateAsObservable();
        }

        /// <summary>
        /// <c>OnNext(List&lt;TypedLobbyInfo&gt;)</c> - ILobbyCallbacks.OnLobbyStatisticsUpdate
        /// </summary>        
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
