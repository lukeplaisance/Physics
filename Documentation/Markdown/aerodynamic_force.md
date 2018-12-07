# Implementing Aerodynamic Force

## Setting Up The Class

Lets start by declaring a Vector3 called "aeroForce". This will be the variable that will hold the active force of the air against the object. Then we can declare references to the particles that we will be using.

```C#
public Vector3 aeroForce; //active force of the air

    public Particle r1; //particle 1
    public Particle r2; //particle 2
    public Particle r3; //particle 3

    public AeroDynamicForce(Particle p1, Particle p2, Particle p3)
    {
        r1 = p1;
        r2 = p2;
        r3 = p3;
        aeroForce = new Vector3(0, 0, 1);
    }
```
## **Method To Check Particle's name**

Next we will create a method that will check the particle's name. We will use this later inside another method for the cloth.

```C#
    public bool CheckParticles(Particle par)
    {
        return par.name == r1.name || par.name == r2.name || par.name == r3.name;
    }
```

## **The Update Method**

Then we will move onto the Update method. This is where all the math happens for the aerodynamics. We will first calculate the average velocity of all the particles.

```C#
    public void Update()
    {
        //calculate the average velocity of the particles
        var Vs = (r1.Velocity + r2.Velocity + r3.Velocity) / 3;
        var V = Vs - aeroForce;
```

Then, calculate the normals of the triangles.

```C#
        //calculate the normal of the triangle
        var diffofR2andR1 = r2.Position - r1.Position;
        var diffofR3andR1 = r3.Position - r1.Position;
        var cross = Vector3.Cross(diffofR2andR1, diffofR3andR1);

        var n = cross / cross.magnitude;
```

Next, calculate the area of the triangle.

```C#
        //calculate the area of the triangle
        var ao = cross.magnitude / 2;
        var a = ao + (Vector3.Dot(V, n) / V.magnitude);
```

Lastly, calculate the total force being applied.

```C#
    //calculate the total force being applied
        var force = -.5f * ((V.magnitude * Vector3.Dot(V, cross)) / (2 * cross.magnitude)) * cross.normalized;
        r1.AddForce(force / 3);
        r2.AddForce(force / 3);
        r3.AddForce(force / 3);
    }
```

With all of this done, we now have a class that implements a simulated wind.

### **Source**

Link to repository this project was built from.

[Link To Repository](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/Cloth)
