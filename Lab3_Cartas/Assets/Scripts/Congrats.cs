using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Congrats : MonoBehaviour

{
    AudioSource somMario;
    // Start is called before the first frame update
    void Start()
    {
        somMario = GetComponent<AudioSource>();
        somMario.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
