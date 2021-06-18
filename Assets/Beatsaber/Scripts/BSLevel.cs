using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;
using Random = UnityEngine.Random;

public class BSLevel : MonoBehaviour
{
    
    [Serializable]
    public class Block
    {
        public float time;
        public GameObject blockObject;
    }
    
    public List<Block> block_sequence;
    public float trace_start_time;
    public int current_index;

    public int block_count;
    public bool tracing;
    public LowerHandle it_handle;
    public BSGamemanagerFinal gamemanager;

    public float music_length;
    public int number_of_bars;
    public float start_delay;
    public GameObject block_prefab;
    public List<GameObject> block_spawn_positions;
    public int prev_block_pos_index;


     void Awake(){
        it_handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        it_handle.FreeRotation();    
    }
    void Start()
    {
        
    }

    public float GeSaStaTi()
    {
        return 5 * music_length / (number_of_bars * 2);
    }

    public void initializeFirstLevel(){
        block_sequence.Clear();
        Block block = new Block();
        block.blockObject = block_spawn_positions[0];
        block.time = 0; 
        block_sequence.Add(block);
        trace_start_time = Time.time;
        tracing = true;
    }

    public void initializeSecondLevel(){
        block_sequence.Clear();

        Block block = new Block();
        block.blockObject = block_spawn_positions[0];
        block.time = 0; 

        Block block2 = new Block();
        block2.blockObject = block_spawn_positions[3];
        block2.time = 0.5f;

        block_sequence.Add(block);
        block_sequence.Add(block2);
        
        trace_start_time = Time.time;
        tracing = true;
    }

    public void initializeThirdLevel()
    {
        trace_start_time = 0; 
        current_index = 0;
        block_count = 0;
        music_length = 96;
        number_of_bars = 48; 
        start_delay = 1; 

        generateRedundantLevel();
        trace_start_time = Time.time;
        tracing = true;

    }
    
    
    public void initializeFinalLevel()
    {
        trace_start_time = 0; 
        current_index = 0;
        block_count = 0;
        music_length = 96;
        number_of_bars = 48; 
        start_delay = 1; 

        generateRandomLevel();
        trace_start_time = Time.time;
        tracing = true;

    }

    public void generateRedundantLevel()
    {
        int[] pos =  {1, 3, 0, 2};
        block_sequence.Clear();
        for (int i = 0; i < number_of_bars * 2; i++)
        {
            if (i % 8 > 3)
            {
                continue;
            }
            Block block = new Block();
            block.blockObject = block_spawn_positions[pos[(i)%4]];
            block.time = i * music_length / (number_of_bars * 2) + start_delay;
            block_sequence.Add(block);
        }
    }

    public void generateRandomLevel()
    {
        block_sequence.Clear();
        for (int i = 0; i < number_of_bars * 2; i++)
        {
            if (i % 8 > 3)
            {
                continue;
            }
            int random;
            do
            {
                random = Random.Range(0, block_spawn_positions.Count);
            } while (random == prev_block_pos_index);
            prev_block_pos_index = random;
            Block block = new Block();
            block.blockObject = block_spawn_positions[random];
            block.time = i * music_length / (number_of_bars * 2) + start_delay;
            block_sequence.Add(block);
        }
    }

   
    
    void Update()
    {
        if (tracing)
        {
            if (Time.time > trace_start_time + block_sequence[current_index].time)
            {
                it_handle.Free();
                it_handle.MoveToPosition(block_sequence[current_index].blockObject.transform.position, 5.0f, false);
                current_index++;
                if (current_index == block_sequence.Count)
                {
                    tracing = false;
                    //gamemanager.StartSabering();
                }
            }
        }
    }
}
