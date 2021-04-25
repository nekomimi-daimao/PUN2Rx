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
    public class ConnectionCallbacksTriggers : ObservableTriggerBase, IConnectionCallbacks
    {
        #region PUN2Callbacks

        private Subject<Unit> onConnected;

        public void OnConnected()
        {
            onConnected?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnConnectedAsObservable()
        {
            return onConnected ?? (onConnected = new Subject<Unit>());
        }

        private Subject<Unit> onConnectedToMaster;

        public void OnConnectedToMaster()
        {
            onConnectedToMaster?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnConnectedToMasterAsObservable()
        {
            return onConnectedToMaster ?? (onConnectedToMaster = new Subject<Unit>());
        }

        private Subject<DisconnectCause> onDisconnected;

        public void OnDisconnected(DisconnectCause cause)
        {
            onDisconnected?.OnNext(cause);
        }

        public IObservable<DisconnectCause> OnDisconnectedAsObservable()
        {
            return onDisconnected ?? (onDisconnected = new Subject<DisconnectCause>());
        }

        private Subject<RegionHandler> onRegionListReceived;

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            onRegionListReceived?.OnNext(regionHandler);
        }

        public IObservable<RegionHandler> OnRegionListReceivedAsObservable()
        {
            return onRegionListReceived ?? (onRegionListReceived = new Subject<RegionHandler>());
        }

        private Subject<Dictionary<string, object>> onCustomAuthenticationResponse;

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            onCustomAuthenticationResponse?.OnNext(data);
        }

        public IObservable<Dictionary<string, object>> OnCustomAuthenticationResponseAsObservable()
        {
            return onCustomAuthenticationResponse ?? (onCustomAuthenticationResponse = new Subject<Dictionary<string, object>>());
        }

        private Subject<Unit> onCustomAuthenticationFailed;

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            onCustomAuthenticationFailed?.OnError(new PUN2Exception(debugMessage));
        }

        public IObservable<Unit> OnCustomAuthenticationFailedAsObservable()
        {
            return onCustomAuthenticationFailed ?? (onCustomAuthenticationFailed = new Subject<Unit>());
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
            onConnected?.OnCompleted();
            onConnectedToMaster?.OnCompleted();
            onDisconnected?.OnCompleted();
            onRegionListReceived?.OnCompleted();
            onCustomAuthenticationResponse?.OnCompleted();
            onCustomAuthenticationFailed?.OnCompleted();
        }

        #endregion
    }
}