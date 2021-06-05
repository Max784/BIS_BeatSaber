using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;

public class BSLevel : MonoBehaviour
{
    // Start is called before the first frame update
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
    public BSGamemanager1 gamemanager;


    public void TraceBlocks()
    {
        trace_start_time = Time.time;
        tracing = true;
        current_index = 0;
        block_count = 0;
    }
    
    void Awake(){
        it_handle = GameObject.Find("Panto").GetComponent<LowerHandle>();
        it_handle.FreeRotation();    
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tracing)
        {
            if (Time.time > trace_start_time + block_sequence[current_index].time)
            {
                it_handle.SwitchTo(block_sequence[current_index].blockObject);
                current_index++;
                if (current_index == block_sequence.Count)
                {
                    tracing = false;
                    gamemanager.StartSabering();
                }
            }
        }
    }
}
