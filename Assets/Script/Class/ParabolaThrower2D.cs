using UnityEngine;

public class ParabolaThrower2D:IParabolaThrower2D
{
    [System.Obsolete]
    public void Throw(Rigidbody2D rb, Vector2 start, Vector2 target, float height)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        Vector2 toTarget = target - start;

        float apexHeight = Mathf.Max(height, toTarget.y + 0.1f);

        float time =
            Mathf.Sqrt(2 * apexHeight / gravity) +
            Mathf.Sqrt(2 * Mathf.Max(apexHeight - toTarget.y, 0.1f) / gravity);

        if (time <= 0) time = 0.1f;

        Vector2 velocityY = Vector2.up * Mathf.Sqrt(2 * gravity * apexHeight);
        Vector2 velocityXZ = toTarget / time;

        rb.velocity = velocityXZ + velocityY;
    }
}
