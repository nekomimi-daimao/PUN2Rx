using System;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UniRx.Triggers;

namespace PUN2Rx
{
    public class PunOwnershipCallbacksTriggers : ObservableTriggerBase, IPunOwnershipCallbacks
    {
        #region PUN2Callbacks

        private Subject<Tuple<PhotonView, Player>> onOwnershipRequest;

        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            onOwnershipRequest?.OnNext(Tuple.Create<PhotonView, Player>(targetView, requestingPlayer));
        }

        public IObservable<Tuple<PhotonView, Player>> OnOwnershipRequestAsObservable()
        {
            return onOwnershipRequest ?? (onOwnershipRequest = new Subject<Tuple<PhotonView, Player>>());
        }

        private Subject<Tuple<PhotonView, Player>> onOwnershipTransferred;

        // TODO ...typo? Transferred?
        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
            onOwnershipTransferred?.OnNext(Tuple.Create(targetView, previousOwner));
        }

        public IObservable<Tuple<PhotonView, Player>> OnOwnershipTransferredAsObservable()
        {
            return onOwnershipTransferred ?? (onOwnershipTransferred = new Subject<Tuple<PhotonView, Player>>());
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
            onOwnershipRequest?.OnCompleted();
            onOwnershipTransferred?.OnCompleted();
        }

        #endregion
    }
}