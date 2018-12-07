# **Creating A Spring Damper**

## **Setting Up the Class**

First, we set up the variables that we will be using. We will need variables for the spring constant, the damping factor, the rest length, and two references to the particle class.

```C#
public class SpringDamper
{
    public float ks, kd; //spring constant, damping factor
    private float lo; //Rest length
    public Particle p1, p2; //particle 1, particle 2

    public SpringDamper(Particle a, Particle b)
    {
        p1 = a;
        p2 = b;
        lo = Vector3.Distance(p1.Position, p2.Position);
        ks = 50;
        kd = 2;
    }
```

## **Method To Check the Particle's name**

This method is the same method as in the AerodynamicForce class, but it is used for the Particle references in this class. This method returns name of particle1 and particle2.

```C#
    public bool CheckParticles(Particle particle)
    {
        return particle.name == p1.name || particle.name == p2.name;
    }
```

## **The Update Method**

The update method is where all our calculations will happen. First, we will calculate the unit length vector between the two particles.

```C#
    public void Update()
    {
        //calculate the unit length vector between the two particle
        var ePrime = p2.Position - p1.Position;
        var l = ePrime.magnitude;
        var e = ePrime.normalized;
```

Next, we will take the velocities of the particles and convert them into 1D by calculating the cross product between "e" and the particle's velocity.

```C#
        //calculate the 1D velocities
        var v1 = Vector3.Dot(e, p1.Velocity);
        var v2 = Vector3.Dot(e, p2.Velocity);
```

Once that is done, we will then convert the 1D velocities back to 3D by calculating the force for the spring constant and the damping factor. Lastly, we will add the calculated forces to our particles.

```C#
        //convert from 1D to 3D
        float fs = -ks * (lo - l);
        float fd = -kd * (v1 - v2);
        var f1 = (fs + fd) * e;
        var f2 = -f1;

        p1.AddForce(f1);
        p2.AddForce(f2);
    }
}
```

## **Spring Damper Behavior**

Now that we have the SpringDamper class set up, we will need to give it functionality. We can start by creating another script and name it "SpringDamperBehavior". This also be a MonoBehavior.

We will first declare references from the SpringDamper class and the ParticleBehavior class. Then, in the Start function, assign p1's anchor to "true" and new up a SpringDamper.

```C#

public class SpringDamperBehavior : MonoBehavior
{
    [SerializeField]
    public SpringDamper springDamper;
    public ParticleBehavior p1, p2;

    void Start()
    {
        p1.particle.isAnchor = true;
        springDamper = new SpringDamper(p1.particle, p2.particle);
    }
```

## **The Update Function**

In the Update function, we will simply call the update function for the SpringDamper reference.

```C#
    void Update()
    {
        springDamper.Update();
    }
```

Once that is done, you now have a spring damper that is ready to be hooked up into unity.

### **Source**

Link to repository this project was built from.

[Link To Repository](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/Cloth)