# **Implementing AABB Collision**

## **What does AABB Stand For?**
- Axiel
- Align
- Bounding
- Box

## **Creating The Collision Volume**

The collision volume is quite simple to make. We can first start by creating vectors for the min, the max, and the size of the volume. We can also create a boolean for if it is colliding or not.

```C#
public class CollisionVolume : MonoBehaviour
{
    public Vector3 min;
    public Vector3 max;
    public Vector3 size;
    public bool isColliding = false;
```

## **The Update Method**

Now that we have our variables, we need to assign the min and max. We will assign them to the game object's position minus the size for the min and plus the size for the max. Note that we our only doing this for the X and Y axis.

```C#
    private void Update()
    {
        min.x = transform.position.x - size.x;
        max.x = transform.position.x + size.x;
        min.y = transform.position.x - size.y;
        max.y = transform.position.x + size.y;
    }
}
```

## **The SortandSweepBehavior Method**

Once our collision volume is set up, we need to be able to control it and determine if the volume is colliding with another. We will first start by making a list of all the volumes in the scene. Then create two more lists, one for active list and one for the closed list.

```C#
public class SortAndSweepBehavior : MonoBehaviour
{
    public List<CollisionVolume> AllVolumes = new List<CollisionVolume>();
    public List<CollisionVolume> activeList;
    public List<CollisionVolume> closedList;
```

## **The Start Method**

The start method is quite simple. here is where we will new up the active list and the closed list.

```C#
    private void Start()
    {
        activeList = new List<CollisionVolume>();
        closedList = new List<CollisionVolume>();
    }
```

## **The CheckCollision Method**

This method will check for any collision on the X and Y axis. We will first loop through all the volumes, and if the active list does not contain the volume, add it.

```C#
    public void CheckCollision()
    {
        for(int i = 0; i < AllVolumes.Count; i++)
        {
            if (!activeList.Contains(AllVolumes[i]))
                activeList.Add(AllVolumes[i]);
```

Now that we have some volumes added to our active list, we then need to check if the list has more than two volumes. if it does, we will then check if the first volume's min on the X axis is less than the second volume's max on the X axis, and if the first volume's max on the X axis is greater than the second volume's min on the X axis. If so, we will then check the same thing for the Y axis. If it meets both of those checks, that means that we have a collision on both axis. If it only meets the second check, that means that there is a collision on the Y axis but not the X.

```C#
            if (activeList.Count >= 2)
            {
                if (activeList[0].min.x < activeList[1].max.x && activeList[0].max.x > activeList[1].min.x) 
                {
                    if(activeList[0].min.y < activeList[1].max.y && activeList[0].max.y > activeList[1].min.y)
                    {
                        Debug.Log("collision");
                        activeList[0].isColliding = true;
                        activeList[1].isColliding = true;
                        closedList.Add(activeList[0]);
                        activeList.Remove(activeList[0]);
                    }
                    else
                    {
                        Debug.Log("collision on Y but no collision on X");
                        activeList[0].isColliding = false;
                        activeList[1].isColliding = false;
                        closedList.Add(activeList[0]);
                        activeList.Remove(activeList[0]);
                    }
                }
```

Within the first "if statement" will do another check to see if the first volume's min on the Y axis is less than the second volume's max on the Y axis, and if the first volume's max on the Y axis is greater than the second volume's min on the Y axis on its own. If so, the means there is a collision on the X axis, but not the Y axis. If the check meets none of these requirements, that means that was no collision.

```C#
                else if (activeList[0].min.y < activeList[1].max.y && activeList[0].max.y > activeList[1].min.y)
                {
                    Debug.Log("collision on X but no collision on Y");
                    activeList[0].isColliding = false;
                    activeList[1].isColliding = false;
                    closedList.Add(activeList[0]);
                    activeList.Remove(activeList[0]);
                }
                else
                {
                    Debug.Log("No Collision");
                    activeList[0].isColliding = false;
                    activeList[1].isColliding = false;
                    closedList.Remove(AllVolumes[i]);
                }
            }
        }
    }
}
```

Using these methods in Unity, I have not been able to show a clear and correct build of AABB working. However I can show a gif in the Unity editor. It will show in the active list that "CubeA" and "CubeB" at the start, and when I move the a cube inside the other, their volumes get added to the closed list.

![alt text](https://raw.githubusercontent.com/lukeplaisance/Physics/master/Documentation/AABB_gif.gif "AABB")

### **Source**

Link to repository this project was built from.

[Link To Repository](https://github.com/lukeplaisance/Physics/tree/master/Assets/Scripts/AABB)

