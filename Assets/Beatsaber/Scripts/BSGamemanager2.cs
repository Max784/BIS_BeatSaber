using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks; 

public class BSGamemanager2 : BSGamemanager1
{
    async public override void PlayIntroduction(){
        await speechOut.Speak("Welcome to the second Level!");
        await speechOut.Speak("This time there are two blocks. Destroy them! But pay attention to the order!");
        level.TraceBlocks();
    }

    async public override void FinishedLevel()
    {
        if(level.block_count == 2){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
        } else{
            await speechOut.Speak("You missed something!");
        }
        
    }
}
