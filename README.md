# PUN2Rx
Convert [PUN 2](https://www.photonengine.com/en-US/pun)'s callback to [UniRx](https://github.com/neuecc/UniRx)'s Operator.

## Descriotion
*Photon Unity Networking 2* has many callbacks.
Using PUN2Rx, You can handle them very easily.
You don't need to inherit and implement photon in your class.
PUN2Rx classified them into two types - OnNext only, OnError only.
See XMLdoc or [table](#operators).

*WARN: Task isn't supported yet.*

## Import
[Release](https://github.com/nekomimi-daimao/PUN2Rx/releases)  
download .unitypackage

## Requirement
- .NET 4.6
- PUN2 v2.7
    - [AssetStore](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922)
    - [official](https://www.photonengine.com/en-US/pun)

- UniRx 6.2.2
    - [AssetStore](https://assetstore.unity.com/packages/tools/integration/unirx-reactive-extensions-for-unity-17276)
    - [github](https://github.com/neuecc/UniRx)

*NOTE: Even different versions, it works. Maybe I think...*

## Example
### merge success & failure callback

```c#
var successStream = this.OnCreateRoomAsObservable();
var failureStream = this.OnCreateRoomFailedAsObservable();
successStream.Merge(failureStream)
    .Subscribe(
        unit => { Debug.Log("success!"); },
        exception => { Debug.Log("failed..."); }
    );
```

### connect and join, catch error
```c#
// room name you want to join
var roomName = "roomName";

// OnNext --- （ConnectedToMaster => OnJoinedRoom）
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


// OnError --- （OnDisconnected or OnJoinRoomFailed）
var failureStream = this.OnDisconnectedAsObservable()
    .Where(cause => cause != DisconnectCause.None && cause != DisconnectCause.DisconnectByClientLogic)
    .Select(cause =>
    {
        throw PUN2Exception.Create((short) cause, cause.ToString());
        return Unit.Default;
    }).Merge(this.OnCreateRoomFailedAsObservable(), this.OnJoinRoomFailedAsObservable());

Observable.Amb(successStream, failureStream)
    .Subscribe(
        unit => { Debug.Log("join room finish!"); },
        exception => { Debug.Log(exception); })
    .AddTo(this);

Debug.Log("connect!");
PhotonNetwork.ConnectUsingSettings();
```

## Operators
Operator | Call | Type
--- | :---: | ---
`Photon.Realtime.IConnectionCallbacks` | -  | - 
OnConnectedAsObservable | OnNext | Unit
OnConnectedToMasterAsObservable | OnNext | Unit
OnDisconnectedAsObservable | OnNext | DisconnectCause
OnRegionListReceivedAsObservable | OnNext | RegionHandler
OnCustomAuthenticationResponseAsObservable | OnNext | Tuple&lt;string, object&gt;
OnCustomAuthenticationFailedAsObservable | **OnError** | Unit
`Photon.Realtime.IInRoomCallbacks` | - | -
OnPlayerEnteredRoomAsObservable | OnNext | Player
OnPlayerLeftRoomAsObservable | OnNext | Player
OnRoomPropertiesUpdateAsObservable | OnNext | HashTable
OnPlayerPropertiesUpdateAsObservable | OnNext | Tuple&lt;PLayer, Hashtable&gt;
OnMasterClientSwitchedAsObservable | OnNext | Player
`Photon.Realtime.ILobbyCallbacks` | |
OnJoinedLobbyAsObservable | OnNext | Unit
OnLeftLobbyAsObservable | OnNext | Unit
OnRoomListUpdateAsObservable | OnNext | List&lt;RoomInfo&gt;
OnLobbyStatisticsUpdateAsObservable | OnNext | List&lt;TypedLobbyInfo&gt;
`Photon.Realtime.IMatchmakingCallbacks` | |
OnFriendListUpdateAsObservable | OnNext | List&lt;FriendInfo&gt;
OnCreateRoomAsObservable | OnNext | Unit
OnCreateRoomFailedAsObservable | **OnError** | Unit
OnJoinedRoomAsObservable | OnNext | Unit
OnJoinRoomFailedAsObservable | **OnError** | Unit
OnJoinRandomFailedAsObservable | **OnError** | Unit
OnLeftRoomAsObservable | OnNext | Unit
`Photon.Pun.IPunOwnershipCallbacks` | |
OnOwnershipRequestAsObservable | OnNext | Tuple&lt;PhotonView, Player&gt;
OnOwnershipTransferredAsObservable | OnNext | Tuple&lt;PhotonView, Player&gt;

## Author
[nekomimi-daimao](https://qiita.com/nekomimi-daimao)

## LICENCE
[MIT](https://github.com/nekomimi-daimao/PUN2Rx/blob/master/LICENSE)
