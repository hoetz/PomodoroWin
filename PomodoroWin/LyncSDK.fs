module LyncSDK

open Microsoft.Lync.Controls
open Microsoft.Lync.Model
open System.Collections.Generic

let dndLyncState=ContactAvailability.DoNotDisturb

let setLyncStatus dndLyncState =
            let  _LyncClient =  Microsoft.Lync.Model.LyncClient.GetClient()
            let contactInfo = new Dictionary<Microsoft.Lync.Model.PublishableContactInformationType, System.Object>()
            contactInfo.Add(Microsoft.Lync.Model.PublishableContactInformationType.Availability, dndLyncState)
            let ar=_LyncClient.Self.BeginPublishContactInformation(contactInfo, null, null);
            _LyncClient.Self.EndPublishContactInformation(ar)
