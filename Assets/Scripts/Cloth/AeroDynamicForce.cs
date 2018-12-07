using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;

public class AeroDynamicForce
{
    public Vector3 aeroForce; //active force of the air

    public Particle r1; //particle 1
    public Particle r2; //particle 2
    public Particle r3; //particle 3

    public AeroDynamicForce(Particle p1, Particle p2, Particle p3)
    {
        r1 = p1;
        r2 = p2;
        r3 = p3;
        aeroForce = new Vector3(0, 0, 0);
    }

    public bool CheckParticles(Particle par)
    {
        return par.name == r1.name || par.name == r2.name || par.name == r3.name;
    }

    public void Update()
    {
        //calculate the average velocity of the particles
        var Vs = (r1.Velocity + r2.Velocity + r3.Velocity) / 3;
        var V = Vs - aeroForce;

        //calculate the normal of the triangle
        var diffofR2andR1 = r2.Position - r1.Position;
        var diffofR3andR1 = r3.Position - r1.Position;
        var cross = Vector3.Cross(diffofR2andR1, diffofR3andR1);

        var n = cross / cross.magnitude;

        //calculate the area of the triangle
        var ao = cross.magnitude / 2;
        var a = ao + (Vector3.Dot(V, n) / V.magnitude);

        //calculate the total force being applied
        var force = -.5f * ((V.magnitude * Vector3.Dot(V, cross)) / (2 * cross.magnitude)) * cross.normalized;
        r1.AddForce(force / 3);
        r2.AddForce(force / 3);
        r3.AddForce(force / 3);
    }
}
