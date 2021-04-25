using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace PUN2Rx
{
    [DisallowMultipleComponent]
    public class InRoomCallbacksTriggers : ObservableTriggerBase, IInRoomCallbacks
    {
        #region PUNCallbacks

        private Subject<Player> onPlayerEnteredRoom;

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            onPlayerEnteredRoom?.OnNext(newPlayer);
        }

        public IObservable<Player> OnPlayerEnteredRoomAsObservable()
        {
            return onPlayerEnteredRoom ?? (onPlayerEnteredRoom = new Subject<Player>());
        }

        private Subject<Player> onPlayerLeftRoom;

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            onPlayerLeftRoom?.OnNext(otherPlayer);
        }

        public IObservable<Player> OnPlayerLeftRoomAsObservable()
        {
            return onPlayerLeftRoom ?? (onPlayerLeftRoom = new Subject<Player>());
        }

        private Subject<Hashtable> onRoomPropertiesUpdate;

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            onRoomPropertiesUpdate?.OnNext(propertiesThatChanged);
        }

        public IObservable<Hashtable> OnRoomPropertiesUpdateAsObservable()
        {
            return onRoomPropertiesUpdate ?? (onRoomPropertiesUpdate = new Subject<Hashtable>());
        }

        private Subject<Tuple<Player, Hashtable>> onPlayerPropertiesUpdate;

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            onPlayerPropertiesUpdate?.OnNext(Tuple.Create(targetPlayer, changedProps));
        }

        public IObservable<Tuple<Player, Hashtable>> OnPlayerPropertiesUpdateAsObservable()
        {
            return onPlayerPropertiesUpdate ?? (onPlayerPropertiesUpdate = new Subject<Tuple<Player, Hashtable>>());
        }

        private Subject<Player> onMasterClientSwitched;

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            onMasterClientSwitched?.OnNext(newMasterClient);
        }

        public IObservable<Player> onMasterClientSwitchedAsObservable()
        {
            return onMasterClientSwitched ?? (onMasterClientSwitched = new Subject<Player>());
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
            onPlayerEnteredRoom?.OnCompleted();
            onPlayerLeftRoom?.OnCompleted();
            onRoomPropertiesUpdate?.OnCompleted();
            onPlayerPropertiesUpdate?.OnCompleted();
            onMasterClientSwitched?.OnCompleted();
        }

        #endregion
    }
}
