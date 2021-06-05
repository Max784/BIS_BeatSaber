using System.Collections;
using System.Collections.Generic;
using SpeechIO;
using UnityEngine;

public class BSGamemanager : MonoBehaviour
{


    private SpeechOut speechOut;
    public BSLevel level;
    public BSPlayer player;

    void Awake()
    {
        speechOut = new SpeechOut();
    }
    
    // Start is called before the first frame update
    async void Start()
    {
        await speechOut.Speak("Welcome to Beatsaber! ");
        level.TraceBlocks();
    }

    async public void StartSabering()
    {
        await speechOut.Speak("Your turn!");
        player.StartSabering();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
