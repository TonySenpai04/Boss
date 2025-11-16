using UnityEngine;

public class Spawner:ISpawner
{
    private readonly GameObject prefab;
    private readonly Transform spawnPoint;
    private readonly IParabolaThrower2D thrower;

    public Spawner(GameObject stonePrefab, Transform spawnPoint)
    {
        prefab = stonePrefab;
        this.spawnPoint = spawnPoint;
        thrower = new ParabolaThrower2D();
    }

    [System.Obsolete]
    public void Spawn(Transform target, float curveHeight)
    {
        if (target == null) return;

        Vector2 start = spawnPoint ? spawnPoint.position : Vector2.zero;
        Vector2 end = target.position;

        GameObject stone = Object.Instantiate(prefab, start, Quaternion.identity);

        if (stone.TryGetComponent(out Rigidbody2D rb))
        {
            thrower.Throw(rb, start, end, curveHeight);
        }
    }
}

