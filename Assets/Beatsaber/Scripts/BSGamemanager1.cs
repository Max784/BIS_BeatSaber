using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks; 

public class BSGamemanager1 : MonoBehaviour
{


    public SpeechOut speechOut;
    public BSLevel level;
    public BSPlayer player;

    public virtual void Awake()
    {
        speechOut = new SpeechOut();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayIntroduction();
    }

    async public virtual void PlayIntroduction(){
        await speechOut.Speak("Welcome to Beatsaber! ");
        await speechOut.Speak("This is your sword!");
        showSword();
        await Task.Delay(1000);
        speechOut.Speak("This is a block. You have to saber it!");
        await Task.Delay(500);
        //level.TraceBlocks();

    }

    async void showSword(){
        player.transform.position = new Vector3(0f,0f,-12f); 
        await player.upperHandle.MoveToPosition(player.transform.position);
    }


    async public void StartSabering()
    {
        await speechOut.Speak("Your turn!");
        player.StartSabering();
    }

    async public virtual void FinishedLevel()
    {
        level.it_handle.Free();
        if(level.block_count == 1){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
        } else{
            await speechOut.Speak("You failed...!");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
