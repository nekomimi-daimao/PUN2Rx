using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    /// <summary>
    /// <see cref="Photon.Realtime.IMatchmakingCallbacks"/>
    /// </summary>
    public static class MatchmakingCallbacksTriggersExtension
    {
        /// <summary>
        /// <c>OnNext(List&lt;FriendInfo&gt;)</c> - IMatchmakingCallbacks.OnFriendListUpdate
        /// </summary>
        public static IObservable<List<FriendInfo>> OnFriendListUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<List<FriendInfo>>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnFriendListUpdateAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Unit)</c> - IMatchmakingCallbacks.OnCreateRoom
        /// </summary>
        public static IObservable<Unit> OnCreateRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnCreatedRoomAsObservable();
        }

        /// <summary>
        /// <c>OnError(Unit)</c> - IMatchmakingCallbacks.OnCreateRoom
        /// </summary>
        public static IObservable<Unit> OnCreateRoomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnCreateRoomFailedAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Unit)</c> - IMatchmakingCallbacks.OnJoinedRoom
        /// </summary>
        public static IObservable<Unit> OnJoinedRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinedRoomAsObservable();
        }

        /// <summary>
        /// <c>OnError(Unit)</c> - IMatchmakingCallbacks.OnJoinRoomFailed
        /// </summary>
        public static IObservable<Unit> OnJoinRoomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinRoomFailedAsObservable();
        }

        /// <summary>
        /// <c>OnError(Unit)</c> - IMatchmakingCallbacks.OnJoinRandomFailed
        /// </summary>
        public static IObservable<Unit> OnJoinRandomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinRandomFailedAsObservable();
        }

        /// <summary>
        /// <c>OnNext(Unit)</c> - IMatchmakingCallbacks.OnLeftRoom
        /// </summary>
        public static IObservable<Unit> OnLeftRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnLeftRoomAsObservable();
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