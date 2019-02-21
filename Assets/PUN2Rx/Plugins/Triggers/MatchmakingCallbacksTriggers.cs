using System;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PUN2Rx
{
    [DisallowMultipleComponent]
    public class MatchmakingCallbacksTriggers : ObservableTriggerBase, IMatchmakingCallbacks
    {
        #region PUNCallbacks

        private Subject<List<FriendInfo>> onFriendListUpdate;

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            onFriendListUpdate?.OnNext(friendList);
        }

        public IObservable<List<FriendInfo>> OnFriendListUpdateAsObservable()
        {
            return onFriendListUpdate ?? (onFriendListUpdate = new Subject<List<FriendInfo>>());
        }

        private Subject<Unit> onCreatedRoom;

        public void OnCreatedRoom()
        {
            onCreatedRoom?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnCreatedRoomAsObservable()
        {
            return onCreatedRoom ?? (onCreatedRoom = new Subject<Unit>());
        }

        private Subject<Unit> onCreateRoomFailed;

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            onCreateRoomFailed?.OnError(PUN2Exception.Create(returnCode, message));
        }

        public IObservable<Unit> OnCreateRoomFailedAsObservable()
        {
            return onCreateRoomFailed ?? (onCreateRoomFailed = new Subject<Unit>());
        }

        private Subject<Unit> onJoinedRoom;

        public void OnJoinedRoom()
        {
            onJoinedRoom?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnJoinedRoomAsObservable()
        {
            return onJoinedRoom ?? (onJoinedRoom = new Subject<Unit>());
        }

        private Subject<Unit> onJoinRoomFailed;

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            onJoinRoomFailed?.OnError(PUN2Exception.Create(returnCode, message));
        }

        public IObservable<Unit> OnJoinRoomFailedAsObservable()
        {
            return onJoinRoomFailed ?? (onJoinRoomFailed = new Subject<Unit>());
        }

        private Subject<Unit> onJoinRandomFailed;

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            onJoinRandomFailed?.OnError(PUN2Exception.Create(returnCode, message));
        }

        public IObservable<Unit> OnJoinRandomFailedAsObservable()
        {
            return onJoinRandomFailed ?? (onJoinRandomFailed = new Subject<Unit>());
        }

        private Subject<Unit> onLeftRoom;

        public void OnLeftRoom()
        {
            onLeftRoom?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnLeftRoomAsObservable()
        {
            return onLeftRoom ?? (onLeftRoom = new Subject<Unit>());
        }

        #endregion

        #region lifecycle

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        #endregion

        #region UniRx

        protected override void RaiseOnCompletedOnDestroy()
        {
            onFriendListUpdate?.OnCompleted();
            onCreatedRoom?.OnCompleted();
            onCreateRoomFailed?.OnCompleted();
            onJoinedRoom?.OnCompleted();
            onJoinRoomFailed?.OnCompleted();
            onJoinRandomFailed?.OnCompleted();
            onLeftRoom?.OnCompleted();
        }

        #endregion
    }
}