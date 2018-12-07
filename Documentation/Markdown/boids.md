# **Implementing the Boids Algorithm**

## **Setting Up The Class**

To start things off we need first declare the variables that we throughout the algorithm. We will need a list of Particles, a list of game objects, a float value for the flock, and three toggles for the boid rules.

```C#
public class BoidBehaviour : MonoBehaviour
{
    [SerializeField]
    public List<ParticleData> Boids;
    public List<GameObject> gameObjects;
    public float flock = 1.0f;
    public Toggle v1toggle;
    public Toggle v2toggle;
    public Toggle v3toggle;
```

## **The Start Method**

The start method doesn't need much. All we need is to set the boid's position from a range of 5 to 10 units away from each other on start.

```C#
    //resets the boid's position on start
    private void Start()
    {
        foreach (var p in Boids)
        {
            p.Position = new Vector3(0, Random.Range(5, 10), 0);
        }

    }
```

## **The LateUpdate Method**

In the LateUpdate function we will simply the "MoveBoidsToNewPosition" method that we are going to get into. Then for each boid, we will draw a line from its position, to the position plus the velocity. This will show what direction the force is coming from when its acting on the boid.

```C#
    private void LateUpdate()
    {
        MoveBoidsToNewPosition();
        foreach (var b in Boids)
        {
            Debug.DrawLine(b.Position, b.Position + b.Velocity, Color.green, Time.deltaTime);
        }
    }
```

## **The BoundPosition Method**

This method is going to set boundaries for the boids. However, this method in action won't be as if there was a wall there. It is more like giving the boids a "gentle persuasion" to stay inside the bounds. This will give a more natural look to the flock.

So first we will declare the min and max for all three axis, a variable for the ground level, and a Vector3 for the perceived velocity. 

```C#
    public Vector3 Bound_Position(ParticleData b)
    {
        //min and max positions
        float Xmin = -100, Xmax = 100, Ymin = 1, Ymax = 100, Zmin = -100, Zmax = 100;

        float groundLevel = 0;

        Vector3 v = Vector3.zero;
```

Once that is done, we will do a check to see if the boid's Y position on less than the ground level. If it is, set the Y position to the ground level.

```C#
        if (b.Position.y < groundLevel)
        {
            b.Position.y = groundLevel;
            b.isPerching = true;
        }
```

Next, we will make checks for the the min and max for each axis. If the position of the boid on that axis is less than the minimum, that position of that axis is set to 100. Vise versa for the max. Once that is done, return the perceived velocity.

```C#
        if (b.Position.x < Xmin)
        {
            v.x = 100;
        }
        else if (b.Position.x > Xmax)
        {
            v.x = -100;
        }

        if (b.Position.y < Ymin)
        {
            v.y = 0;
        }
        else if (b.Position.x > Ymax)
        {
            v.y = -100;
        }

        if (b.Position.y < Zmin)
        {
            v.z = 100;
        }
        else if (b.Position.x > Zmax)
        {
            v.z = -100;
        }

        return v;
    }
```

## **The Boid_Cohesion Method (Rule 1)**

For this method, we will move the boids to the "perceived center" of the flock. We will first declare a variables for the total number of boids and one for the perceived center.

```C#
    public Vector3 Boid_Cohesion(ParticleData b)
    {
        //the number of boids
        var N = Boids.Count;

        //the perceived center
        Vector3 pc = Vector3.zero;
```

Once that is done, we will then loop through the list of Boids and find the average position for each of them.

```C#
        //finds the average position of each boid
        foreach (var item in Boids)
        {
            if (item != b)
            {
                pc += item.Position;
            }
        }
        pc = pc / (N - 1);
        return (pc - b.Position) / 100;
    }
```

## **The Boid_Dispersion Method (Rule 2)**

This rule is made for that the boids won't collide into each other. We will look at each boid, and if their position is within a certain range of each other, they will move in the opposite way. 

```C#
    public Vector3 Boid_Dispersion(ParticleData b)
    {
        //the displacement of each boid
        Vector3 d = Vector3.zero;

        //if the position of a boid is less than or equal to 15 units away from another, 
        //the boid will go the opposite way
        foreach (var item in Boids)
        {
            if (item != b)
                if ((item.Position - b.Position).magnitude <= 15)
                {
                    d = d - (item.Position - b.Position);
                }
        }
        return d;
    }
```

## **The Boid_Alignment Method (Rule 3)**

This method is quite similar to Rule 1, but instead of calculating the boid's average position, we will calculate the boid's average velocity. Then we will add a small portion to the boid's current velocity.

So first, we will declare variables for the number of boids and for the perceived velocity.

```C#
    public Vector3 Boid_Alignment(ParticleData b)
    {
        //the number of boids
        var N = Boids.Count;

        //percieved velocity
        Vector3 pv = Vector3.zero;
```

Once that is done, we will then loop though each boid, and find the average velocity.

```C#
        //finds the average velocity of each boid
        foreach (var item in Boids)
        {
            if (item != b)
            {
                pv += b.Velocity;
            }
        }
        pv = pv / (N - 1);
        return (pv - b.Velocity) / 8;
    }
```

## **The MoveBoidsToNewPosition Method**

This method is where we put all the rules together to make them work. We will start by declaring variables for three boid rules and one for the boundaries.

```C#
    public void MoveBoidsToNewPosition()
    {
        //declare vectors for the 3 boid rules methods and one for the boundaries
        Vector3 v1;
        Vector3 v2;
        Vector3 v3;
        Vector3 v4;
```

Once that is done, we will loop through each boid and check if the boid is "perching" or not. If it is, the perch timer will start.

```C#
        foreach (var b in Boids)
        {
            //checks to see if a boid is perching, if so, the perch timer starts
            if (b.isPerching)
            {
                if (b.perch_timer > 0)
                {
                    b.perch_timer = b.perch_timer - 1;
                }
                else
                {
                    b.Position = new Vector3(0, 30, 0);
                    b.isPerching = false;
                    b.perch_timer = 50;
                }
            }
```

Next is where we will be calling all the rules, In the unity scene, there are three toggles set up to turn on and off each rule, so we will need to make checks for each toggle. If the toggle is on, the rule is active, but if the toggle is off, the rule is negated. After that assign the fourth vector to our boundary method.

```C#
            if (b.perch_timer == 50)
            {
                //checks to see if the toggles are either on or off
                if (v1toggle.isOn)
                {
                    //calls Rule 1
                    v1 = flock * Boid_Cohesion(b);
                }
                else
                    v1 = -flock * Boid_Cohesion(b);

                if (v2toggle.isOn)
                {
                    //calls Rule 2
                    v2 = Boid_Dispersion(b);
                }
                else
                    v2 = -Boid_Dispersion(b);


                b.Velocity += v1 + v2 * Time.deltaTime;


                if (v3toggle.isOn)
                {
                    //calls Rule 3
                    v3 = Boid_Alignment(b);
                    b.Velocity += v1 + v2 + v3 * Time.deltaTime;
                }
                else
                    b.Velocity += v1 + v2 * Time.deltaTime;

                v4 = Bound_Position(b);
```

Lastly, we will be checking the velocity's magnitude. If it is over 10, we will normalize the velocity. Then we will assign the boid's position to the position plus the velocity.

```C#
                if (b.Velocity.magnitude > 10)
                    b.Velocity = b.Velocity.normalized;

                b.Position = b.Position + b.Velocity;
                gameObjects[Boids.IndexOf(b)].transform.position = b.Position;
            }
        }
    }
}
```

Once that is hooked up in unity, the flock should look something like this.

![alt text](https://raw.githubusercontent.com/lukeplaisance/Physics/master/Documentation/boids_gif.gif "Boids")

### **Source**

Link to repository this project was built from.

[Link To Repository](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/LukeTools)