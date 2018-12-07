# **Generating Cloth**

With The Particle class, the SpringDamper class, and the AeroDynamicForce class implemented, we can now throw them all together to generate our cloth.

To start, we will create a list for the particles, the springs, aero dynamic forces(triangles), and for the game objects.

```C#
public class ClothGeneratorBehavior : MonoBehaviour
{
    public List<Particle> Particles = new List<Particle>();
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<SpringDamper> Springs = new List<SpringDamper>();
    public List<AeroDynamicForce> Triangles = new List<AeroDynamicForce>();
```

We will also need references from the SpringDamper and Particle classes, floats for the width and height, and a Vector3 for the world mouse position.

```C#
    private SpringDamper spring;
    public float width;
    public float height;
    private Particle grabbed;
    private Vector3 worldMouse;
```

## **Method To Generate The Cloth**

In this method is where we will create out particles and our springs that connect all of them together.

First we will loop through the width and height to create the particles and game objects. Then add them to their lists.

```C#
public void GenCloth()
{
    for (float x = 0; x < width; x++)
    {
        for (float y = 0; y < height; y++)
        {
            Particles.Add(new Particle(new Vector3(x, y, 0)));
            var newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newObj.transform.position = Particles[Particles.Count - 1].Position;
            gameObjects.Add(newObj);
        }
    }
```

Next we will loop through the "Particles" list and create the springs and cross springs.

```C#
    //adding the spring dampers
    for (int i = 0; i < Particles.Count; i++)
    {
        if (i % width != width - 1)
        {
            Springs.Add(new SpringDamper(Particles[i], Particles[i + 1]));
        }
        if (i < Particles.Count - width)
        {
            Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width];
        }

        if (i % width != width - 1 && i < Particles.Count - width)
        {
            Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width + 1]));
        }
        if (i % width != 0 && i < Particles.Count - height)
        {
            Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width - 1]));
        }
    }

    //adding the cross springs
    for (int i = 0; i < Particles.Count; i++)
    {
        if (i % width != width - 1 && i < Particles.Count - width)
        {
            Triangles.Add(new AeroDynamicForce(Particles[i], Particles[i + 1], Particles[i + (int)width]));
            Triangles.Add(new AeroDynamicForce(Particles[i + 1], Particles[i + (int)width + 1], Particles[i + (int)width]));
        }
    }
```

## **The OnDrawGizmos Method**

We will be using the built in OnDrawGizmos method to draw a line from particle1 to particle2 for each spring.

```C#
    void OnDrawGizmos()
    {
        foreach (var s in Springs)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(s.p1.Position, s.p2.Position);
        }
    }
```

## **The Update Method**

In the update method, this is where we will handle the calculations for grabbing and tearing the cloth. We will start by getting the mouse's position. Then check if the user is clicking and holding the mouse button down, and if the position of the particle and the position of the mouse's position are on top each other.

```C#
    void Update()
    {
        var mousePos = Input.mousePosition;

        worldMouse = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 
                                                        -Camera.main.transform.position.z));

        if(Input.GetMouseButtonDown(0))
        {
            foreach(var p in Particles)
            {
                var scalePosition = new Vector3(p.Position.x * transform.localScale.x, 
                                                    p.Position.y * transform.localScale.y, 
                                                    p.Position.z * transform.localScale.z);
                var checkPos = new Vector3(worldMouse.x, worldMouse.y, p.Position.z);
                if (Vector3.Distance(checkPos, scalePosition) <= 1f)
                        grabbed = p;
            }
        }
```

Now we will check if the particle is grabbed, if so, the particle's position will be the same as the mouse's position. Then we will check again to see of the force acting on the particle, or if the user presses the "A" key, the spring will be removed and the cloth will tear.

```C#
    if(Input.GetMouseButton(0) && grabbed != null)
    {
        grabbed.Position = worldMouse;
        if(grabbed.Force.magnitude >= 5 || Input.GetKeyDown(KeyCode.A))
        {
            grabbed.isActive = true;
            for(var i = 0; i < Springs.Count; i++)
            {
                if(Springs[i].CheckParticles(grabbed))
                {
                    Springs.RemoveAt(i);
                }
            }
            for (var i = 0; i < Triangles.Count; i++)
            {
                if(Triangles[i].CheckParticles(grabbed))
                {
                    Triangles.RemoveAt(i);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
                grabbed.isAnchor = !grabbed.isAnchor;
        }
        if (Input.GetMouseButtonUp(0))
            grabbed = null;   
    }
}
```

## **The LateUpdate Method**

In this method, we will first make a for each loop for the Springs list and the Triangles list and call update for each of them.

```C#
    foreach (var s in Springs)
    {
        s.Update();
    }
    foreach (var force in Triangles)
    {
        force.Update();
    }
```

Then, we will loop through each particle and add gravity onto them. After that call the Update method for the particle.

```C#
    foreach (var particle in Particles)
    {
        var gravity = new Vector3(0, -9.81f, 0);
        particle.AddForce(gravity * .25f);
        particle.Update(Time.deltaTime);
    }
```

Lastly, we will then loop through the gameObjects list and assign the game object's position to the particle's position.

```C#
    for (int i = 0; i < gameObjects.Count; i++)
    {
        gameObjects[i].transform.position = Particles[i].Position;
        Destroy(gameObjects[i].GetComponent("SphereCollider"));
    }
}
```

Once everything is hooked up correctly in unity, it should look something like this.

![alt text](https://raw.githubusercontent.com/lukeplaisance/Physics/master/Documentation/cloth_generated.gif "Generated Cloth")


### **Source**

Link to repository this project was built from.

[Link To Repository](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/Cloth)