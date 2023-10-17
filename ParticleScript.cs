using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    public void ParticleStart(){
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        DontDestroyOnLoad(gameObject);
    }
}
