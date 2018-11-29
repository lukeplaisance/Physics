using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

public class ClothParticleBehaviour : MonoBehaviour
{
    public Particle particle;

	// Use this for initialization
	void Awake ()
    {
        particle = new Particle();
        particle.Position = transform.position;
        particle.Mass = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
  

        var gravity = new Vector3(0, -9.81f, 0);
        particle.AddForce(gravity * 0.25f);
        particle.Update(Time.deltaTime);
        transform.position = particle.Position;
    }
}
