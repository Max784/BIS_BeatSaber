using System;
using System.Collections;
using System.Collections.Generic;
using DualPantoFramework;
using UnityEngine;

public class BSPlayer : MonoBehaviour
{
    public BSLevel level;
    public float sabering_start_time;
    public bool sabering;
    public int current_index;
    public float time_window = 1.0f;
    private UpperHandle upperHandle;

    void Awake()
    {
        upperHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
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

        if (sabering && sabering_start_time + level.block_sequence[current_index].time + time_window < Time.time)
        {
            Debug.Log(":( nooooo");
            current_index++;
            if (current_index == level.block_sequence.Count)
            {
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
                current_index++;
                if (current_index == level.block_sequence.Count)
                {
                    sabering = false;
                }
            }
        }
    }
}
