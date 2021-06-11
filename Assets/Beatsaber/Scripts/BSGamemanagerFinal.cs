using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks;


public class BSGamemanagerFinal : BSGamemanager1
{
    public AudioSource ass;

    public override void Awake()
    {
        base.Awake();
    }
    async public override void PlayIntroduction()
    {
        ass.Play();
        level.TraceBlocks();
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();
    }

    async public override void FinishedLevel()
    {
        level.it_handle.Free();
        if(level.block_count == 2){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
        } else{
            await speechOut.Speak("You missed something!");
        }
        
    }
}
