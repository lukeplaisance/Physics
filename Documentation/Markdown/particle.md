# **Creating A Particle**

## **Setting Up The Properties**

With creating a particle, we need to create properties that are in charge on things like its, velocity, acceleration, displacement between other particles, its mass, and how much force is acting on it.

```C#
public class Particle
{
    public Vector3 Velocity { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Acceleration { get; set; }
    public Vector3 Displacement { get; set;}
    public Vector3 Force { get; set; }
    public float Mass { get; set; }
```

We will also declare variables for if its anchored, if its active, and for a name.

```C#
    public bool isAnchor;
    public bool isActive = true;
    public string name = "null";
}

public Particle(Vector3 pos)
{
    Position = pos;
    Velocity = new Vector3(0, -1, 0);
    Mass = 1;
}
```

## **Method To Add A Force**

This method is quite simple. We will create this method with an argument of a Vector3 and we will call it "force". All you have do is add that force to the particle's force.

```C#
public void AddForce(Vector3 force)
{
    Force += force;
}
```

## **The Update Method**

In the Update method we will first check any of the particles are anchored. If they are, their force will be zero.

```C#
public void Update(float dt)
{

    if (isAnchor)
    {
        Force = Vector3.zero;
        return;
    }
```

Next, we will implement the Euler integration. And once that is done, we will reset the force back to zero.

```C#
Acceleration = Force * Mass;
Velocity = Velocity + Acceleration * dt;
Position += Velocity * dt;

Force = Vector3.zero;
```

Once that is done, we now have the Particle class set up and ready to use for the cloth.

## **ParticleBehavior Class**

Now that we have the Particle class set up, we need to give it some functionality for the springs that we will create. To start, we will declare a reference from the Particle class. Then, in the Awake method, we will new up a particle and assign its position and mass.

```C#
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
```

### **Source**

Link to repository this project was built from.

[Link To Particle](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/LukeTools)

[Link To Particle Behavior](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/Cloth)