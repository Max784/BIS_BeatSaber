using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks;


public class BSGamemanagerFinal : MonoBehaviour
{
    
    public SpeechOut speechOut;
    public BSLevel level;
    public BSPlayer player;
    
    public AudioSource ass;

    private int level_index; 

    public void Awake()
    {
        speechOut = new SpeechOut();

    }

    void Start()
    {
        // PlayIntroduction();
        // PlaySecondLevel(); 
        PlayFinalLevel();
    }

    async public virtual void PlayIntroduction(){
        player.time_window = 10f; 
        level_index = 1; 

        await speechOut.Speak("Welcome to Beatsaber! ");
        await speechOut.Speak("This is your sword!");
        showSword();
        await Task.Delay(1000);
        speechOut.Speak("This is a block. You have to saber it!");
        await Task.Delay(500);
        level.initializeFirstLevel(); 
        StartSabering(); 
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


    async public void PlaySecondLevel(){
        player.time_window = 10f; 
        level_index = 2; 

        await speechOut.Speak("Welcome to the second Level!");
        await speechOut.Speak("This time there are two blocks. Destroy them! But pay attention to the order!");
        level.initializeSecondLevel(); 
        await Task.Delay(500);
        StartSabering(); 
    }

    async void PlayFinalLevel(){
        player.time_window = 0.35f; 
        level_index = 5; 

        ass.Play();
        level.initializeFinalLevel();
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();
    }

    public void FinishedLevel()
    {
        level.it_handle.Free();
        
        switch (level_index)
        {
            case 1:
                FinishedLevelMessage(1);
                break;
            case 2:
                FinishedLevelMessage(2);
                break;
            default:
                
                break;
        }  
    }

    async public void FinishedLevelMessage(int number_of_blocks){
      
        if(level.current_index == number_of_blocks){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
        } else{
            await speechOut.Speak("You missed something!");
        }
   
    }
}
