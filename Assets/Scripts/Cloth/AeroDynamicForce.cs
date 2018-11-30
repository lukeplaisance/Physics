using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

public class AeroDynamicForce
{
    public Vector3 density;
    private float Cd;
    private Vector3 a;
    private Vector3 e;

    public Particle r1;
    public Particle r2;
    public Particle r3;


    void Update()
    {
        //calculate the average velocity of the particles
        var Vs = (r1.Velocity + r2.Velocity + r3.Velocity) / 3;
        var V = Vs - density;

        //calculate the normal of the triangle
        var diffofR2andR1 = r2.Position - r1.Position;
        var diffofR3andR1 = r3.Position - r1.Position;
        var cross = Vector3.Cross(diffofR2andR1, diffofR3andR1);

        var n = cross / cross.magnitude; 

        //calculate the area of the triangle
        var ao = .5f * cross.magnitude;
        var a = ao + (Vector3.Dot(V, n) / V.magnitude);

        //calculate the total force being applied
        //var force = .5 * ((V.magnitude * Vector3.Dot(V, cross)) / (2 * cross.magnitude)) * cross.normalized;
    }
}
