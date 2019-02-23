using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;

namespace PUN2Rx.Example
{
    /// <summary>
    /// sample connect and join room.
    /// You'll get the same log either observable or coroutine.
    /// ...but observable catches exception.
    /// </summary>
    public class ConnectAndJoinExample : MonoBehaviour
    {
        private const string RoomName = "room";

        private void Start()
        {
            // observable
            ConnectAndJoinRoomAsObservable(RoomName)
                .Subscribe(
                    unit => { Debug.Log("join room finish!"); },
                    exception => { Debug.Log(exception); })
                .AddTo(this);

            Debug.Log("connect!");
            PhotonNetwork.ConnectUsingSettings();


            // coroutine
            // StartCoroutine(ConnectAndJoinRoomCoroutine(RoomName));
        }

        IObservable<Unit> ConnectAndJoinRoomAsObservable(string roomName)
        {
            var successStream =
                this.OnConnectedToMasterAsObservable().Take(1).IgnoreElements()
                    .DoOnCompleted(() =>
                        {
                            Debug.Log("connect finish!");
                            Debug.Log("join room!");
                            var roomOptions = new RoomOptions();
                            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
                        }
                    ).Concat(this.OnJoinedRoomAsObservable());

            var failureStream = this.OnDisconnectedAsObservable()
                .Where(cause => cause != DisconnectCause.None && cause != DisconnectCause.DisconnectByClientLogic)
                .Select(cause =>
                {
                    throw PUN2Exception.Create((short) cause, cause.ToString());
                    return Unit.Default;
                }).Merge(this.OnJoinRoomFailedAsObservable());

            return Observable.Amb(successStream, failureStream).Share();
        }

        IEnumerator ConnectAndJoinRoomCoroutine(string roomName)
        {
            var connectToMaster = this.OnConnectedToMasterAsObservable().First().PublishLast();
            connectToMaster.Connect();
            Debug.Log("connect!");
            PhotonNetwork.ConnectUsingSettings();
            yield return connectToMaster.ToYieldInstruction();
            Debug.Log("connect finish!");
            var onCreateRoom = this.OnCreateRoomAsObservable().First().PublishLast();
            onCreateRoom.Connect();
            Debug.Log("join room!");
            var roomOptions = new RoomOptions();
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            yield return onCreateRoom.ToYieldInstruction();
            Debug.Log("join room finish!");
        }
    }
}