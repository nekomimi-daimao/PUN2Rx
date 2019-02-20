using System;
using System.Collections.Generic;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx
{
    public static class MatchmakingCallbacksTriggersExtension
    {
        public static IObservable<List<FriendInfo>> OnFriendListUpdateAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<List<FriendInfo>>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnFriendListUpdateAsObservable();
        }

        public static IObservable<Unit> OnCreateRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnCreatedRoomAsObservable();
        }

        public static IObservable<Unit> OnCreateRoomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnCreateRoomFailedAsObservable();
        }

        public static IObservable<Unit> OnJoinedRoomAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinedRoomAsObservable();
        }

        public static IObservable<Unit> OnJoinRoomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinRoomFailedAsObservable();
        }

        public static IObservable<Unit> OnJoinRandomFailedAsObservable(this Component component)
        {
            return component?.gameObject == null
                ? Observable.Empty<Unit>()
                : GetOrAddComponent<MatchmakingCallbacksTriggers>(component.gameObject).OnJoinRandomFailedAsObservable();
        }

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