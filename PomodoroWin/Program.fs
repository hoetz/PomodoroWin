// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open Microsoft.Lync.Controls
open LyncSDK

let is_numeric a = fst (System.Int32.TryParse(a))

[<EntryPoint>]
let main argv = 
    let isArgValidMinutes=Seq.exists is_numeric argv

    let minutesToGo = 
        if isArgValidMinutes then
            System.Int32.Parse(Seq.head argv)
        else
            25

    let doNotDisturbMe forMinutes =
        
        setLyncStatus dndLyncState
         
        let event = new System.Threading.AutoResetEvent(false)
        let timer = new System.Timers.Timer(float(forMinutes*60*1000))
        timer.Elapsed.Add (fun _ -> event.Set() |> ignore )
        timer.Start()
        let now=System.DateTime.Now.ToString()
        printfn "You're dnd for %i minutes now. Started at %s" forMinutes now
        event.WaitOne() |> ignore

        setLyncStatus ContactAvailability.Free
    
    doNotDisturbMe minutesToGo
    0 // return an integer exit code
