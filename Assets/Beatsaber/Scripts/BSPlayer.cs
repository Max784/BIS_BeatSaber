using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;
using Random = UnityEngine.Random;

public class BSPlayer : MonoBehaviour
{
    public BSLevel level;
    public float sabering_start_time;
    public bool sabering;
    public int current_index;
    public int hit_streak = 0; 
    public float score = 0;
    public float time_window = 1.0f;
    public UpperHandle upperHandle;

    public BSGamemanagerFinal gamemanager;

    public AudioSource hitAudio, missAudio; 

    public AudioClip[] collisionClip;
    public AudioClip lastCollisionClip;


    void Awake()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
        upperHandle.FreeRotation(); 

    }

    public void StartSabering()
    {
        sabering = true;
        sabering_start_time = Time.time;
        current_index = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = upperHandle.HandlePosition(transform.position);

        if (sabering && sabering_start_time + level.block_sequence[current_index].time + time_window< Time.time)
        {
            Debug.Log(":( nooooo");
            hit_streak = 0; 
            current_index++;
            //missAudio.Play();
            if (current_index == level.block_sequence.Count)
            {
                gamemanager.FinishedLevel();
                sabering = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!sabering)
        {
            return;
        }
        if (other.gameObject == level.block_sequence[current_index].blockObject)
        {
            if (sabering_start_time + level.block_sequence[current_index].time - time_window < Time.time)
            {
                Debug.Log("Yay");
                hit_streak++; 
                level.block_count ++;
                PlayHitClip(); 
                score += hit_streak;
                current_index++;
                if (current_index == level.block_sequence.Count)
                {
                    sabering = false;
                    gamemanager.FinishedLevel();
                }
                if ((gamemanager.level_index == 3 || gamemanager.level_index == 4) && current_index % 4 == 0 && hit_streak >= 4) //das dritte Level soll beendet werden, wenn eine Sequenz von vier Blöcken am Stück getroffen wird. Deswegen dieser Sexy Code
                {
                    gamemanager.FinishedLevel();
                    level.tracing = false; 
                    sabering = false; 
                }
            }
        }
    }

    public void PlayHitClip(){
        if (hit_streak > 6){
            hitAudio.pitch = 1.5f;
        } else{
            hitAudio.pitch = hit_streak/4f;
        }
		hitAudio.Play();
    }
}
