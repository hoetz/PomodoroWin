// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open Microsoft.Lync.Controls
open System.Media
open LyncSDK
open System

let is_numeric a = fst (System.Int32.TryParse(a))


let playBell times = 
        let a = System.Reflection.Assembly.GetExecutingAssembly()
        let wavStream = a.GetManifestResourceStream("bell.wav")
        let player=new System.Media.SoundPlayer(wavStream)

        for i=1 to times do
            try
                player.PlaySync()
            with
                | :? System.IO.FileNotFoundException as ex -> printfn "Warning: Sound file not found"


[<EntryPoint>]
let main argv = 

    let minutesToGo = 
        if (Seq.isEmpty argv)=false && is_numeric (Seq.head argv) then
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

        playBell 2
        setLyncStatus ContactAvailability.Free

    doNotDisturbMe minutesToGo
    0 // return an integer exit code
