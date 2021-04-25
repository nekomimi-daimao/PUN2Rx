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
    public class LobbyCallbacksTriggers : ObservableTriggerBase, ILobbyCallbacks
    {
        private Subject<Unit> onJoinedLobby;

        public void OnJoinedLobby()
        {
            onJoinedLobby?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnJoinedLobbyAsObservable()
        {
            return onJoinedLobby ?? (onJoinedLobby = new Subject<Unit>());
        }

        private Subject<Unit> onLeftLobby;

        public void OnLeftLobby()
        {
            onLeftLobby?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnLeftLobbyAsObservable()
        {
            return onLeftLobby ?? (onLeftLobby = new Subject<Unit>());
        }

        private Subject<List<RoomInfo>> onRoomListUpdate;

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            onRoomListUpdate?.OnNext(roomList);
        }

        public IObservable<List<RoomInfo>> OnRoomListUpdateAsObservable()
        {
            return onRoomListUpdate ?? (onRoomListUpdate = new Subject<List<RoomInfo>>());
        }

        private Subject<List<TypedLobbyInfo>> onLobbyStatisticsUpdate;

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            onLobbyStatisticsUpdate?.OnNext(lobbyStatistics);
        }

        public IObservable<List<TypedLobbyInfo>> OnLobbyStatisticsUpdateAsObservable()
        {
            return onLobbyStatisticsUpdate ?? (onLobbyStatisticsUpdate = new Subject<List<TypedLobbyInfo>>());
        }

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
            onJoinedLobby?.OnCompleted();
            onLeftLobby?.OnCompleted();
            onRoomListUpdate?.OnCompleted();
            onLobbyStatisticsUpdate?.OnCompleted();
        }

        #endregion
    }
}