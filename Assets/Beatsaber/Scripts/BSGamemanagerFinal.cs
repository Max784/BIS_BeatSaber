using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;
using System.Threading.Tasks;


public class BSGamemanagerFinal : MonoBehaviour
{

    public enum lvl
    {
        Level1,
        Level2,
        Level3,
        Level4,
        LevelFinal,

        LevelSevenNationArmy
    };
    public static class FadeAudioSource {
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
            {
            float currentTime = 0;
            float start = audioSource.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }

    public lvl level_selection;
    public SpeechOut speechOut;
    public BSLevel level;
    public BSPlayer player;
    
    public AudioSource ass;

    public AudioClip sevenNationArmy; 

    public int level_index; 

    public void Awake()
    {
        speechOut = new SpeechOut();

    }

    void Start()
    {
        switch (level_selection)
        {
            case lvl.Level1:
                PlayIntroduction();
                break;
            case lvl.Level2:
               PlaySecondLevel();
                break;
            case lvl.Level3:
                PlayThirdLevel();
                break;
            case lvl.Level4:
                PlayFourthLevel(); 
                break;
            case lvl.LevelFinal:
                PlayFinalLevel();
                break;
            case lvl.LevelSevenNationArmy:
                PlayLeSeNaArrrrrr();
                break;
        }
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
        player.hit_streak = 4;
        ass.volume = 0.15f;
        ass.Play();
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


        player.hit_streak = 3;
        ass.volume = 0.15f;
        ass.Play();

        level.initializeSecondLevel(); 
        await Task.Delay(500);
        StartSabering(); 
    }

    async public void PlayThirdLevel(){
        player.time_window = 10f; 
        level_index = 3; 

        await speechOut.Speak("Welcome to the third Level!");
        await speechOut.Speak("You must always remember four blocks from now on. Start sabering when the sequence ends. Try to do it on beat!");

        ass.volume = 0.15f;
        ass.Play();

        level.initializeThirdLevel(); 
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();
    }

    async public void PlayFourthLevel(){
        player.time_window = 1f; 
        level_index = 4; 

        await speechOut.Speak("Welcome to the fourth Level!");
        await speechOut.Speak("The same as before, but now you really have to pay attention to the beat! ");

        ass.volume = 0.15f;
        ass.Play();

        level.initializeThirdLevel(); 
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();
    }

    async void PlayFinalLevel(){
        player.time_window = 0.35f; 
        level_index = 5; 

        ass.volume = 0.15f;
        ass.Play();
        level.initializeFinalLevel();
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();
    }

    async void PlayLeSeNaArrrrrr(){
        player.time_window = 0.35f; 
        level_index = 5; 
        ass.clip = sevenNationArmy; 

        ass.volume = 0.45f;
        ass.Play();
        level.initializeLeSeNaArrrrrr();
        await Task.Delay((int)level.GeSaStaTi() * 1000);
        player.StartSabering();

    }

    public void FinishedLevel()
    {
        level.it_handle.Free();
        StartCoroutine(FadeAudioSource.StartFade(ass, 3f, 0f));
        
        switch (level_index)
        {
            case 1:
                FinishedLevelMessage(1);
                break;
            case 2:
                FinishedSecondLevelMessage(2);
                break;
            case 3:
                FinishedThirdLevelMessage();
                break;
            case 4:
                FinishedFourthLevelMessage();
                break;
            case 5: 
                ScoreMessage(player.score);
                break;
            default:
                
                break;
        }  
    }

    async public void ScoreMessage(float score){
        await Task.Delay(2000);
        await speechOut.Speak("You made it!");
        await speechOut.Speak("You got " + score + " Points!");
    }

    async public void FinishedLevelMessage(int number_of_blocks){
      
        if(level.current_index == number_of_blocks){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
            //PlaySecondLevel();
        } else{
            await speechOut.Speak("You missed something!");
            //PlayIntroduction();
        }
    }
    async public void FinishedSecondLevelMessage(int number_of_blocks){
      
        if(level.current_index == number_of_blocks){
            await Task.Delay(500);
            await speechOut.Speak("You made it!");
            //PlayThirdLevel();
        } else{
            await speechOut.Speak("You missed something!");
            //PlaySecondLevel();
        }
    }

    async public void FinishedThirdLevelMessage(){
        await Task.Delay(500);
        await speechOut.Speak("Congratulations! You hit four blocks in a row!");
        //PlayFourthLevel();

    }

    async public void FinishedFourthLevelMessage(){
        await Task.Delay(500);
        await speechOut.Speak("Congratulations! You are a true BeatSaber. Now you are ready for the real world! ");
        //PlayFinalLevel();
       
        
    }

    
}
