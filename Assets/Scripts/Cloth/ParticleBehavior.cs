using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

public class ParticleBehavior : MonoBehaviour
{
    public Particle particle;

    void Awake()
    {
        particle = new Particle(new Vector3(particle.Position.x, particle.Position.y, particle.Position.z));
        particle.Position = transform.position;
        particle.Mass = 1;
    }
}
