using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

public class SpringDamper
{
    private readonly Vector3 Gravity = new Vector3(0, -9.81f, 0);

    private float ks, kd; //spring constant, damping factor
    private float lo; //Rest length
    public float l; //unit length
    private Particle p1, p2; //particle 1, particle 2;
    public Vector3 e; //unit length vector
    public Vector3 ePrime;

    public void Update()
    {
        //calculate the unit length vector between the two particle
        ePrime = p2.Position - p1.Position;
        l = ePrime.magnitude;
        e = ePrime.normalized / l;

        //calculate the 1D velocities
        var v1 = Vector3.Dot(e, p1.Velocity);
        var v2 = Vector3.Dot(e, p2.Velocity);

        //convert from 1D to 3D
        var hooksLaw = -ks * (lo - l) - kd * (v1 - v2);
        var f1 = hooksLaw * e;
        var f2 = -f1;
    }
}
