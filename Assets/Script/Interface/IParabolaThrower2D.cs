using UnityEngine;

public interface IParabolaThrower2D 
{
    [System.Obsolete]
    void Throw(Rigidbody2D rb, Vector2 start, Vector2 target, float height);
}
