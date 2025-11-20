using UnityEngine;

public class FlayingObjectManager : MonoBehaviour
{
    public void DestroyAllFlyingObjects()
    {
        ObstaclesControllerScript[] flyingObjects =
            Object.FindObjectsByType<ObstaclesControllerScript>(
                FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        foreach (ObstaclesControllerScript obj in flyingObjects)
        {
            if (obj == null)
                continue;

            if (obj.CompareTag("Bomb"))
            {
                obj.TriggerExplosion();

            }
            else
            {
                obj.StartToDestroy(Color.cyan);
            }
        }
    }
}
