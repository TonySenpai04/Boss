using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Poison : MonoBehaviour
{
    [Header("Damage Settings")]
    public float stunTime = 2f;   
    public float radius = 2f;      
    public float lifeTime = 3f;    

    [Header("Range Visualization")]
    public bool showRange = true;   
    public int segments = 100;       
    private LineRenderer lr;

    void Awake()
    {
        // Thiết lập LineRenderer
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segments + 1;
        lr.loop = true;
        lr.useWorldSpace = true;     // dùng world space để vòng tròn không bị méo khi stone rotate/scale
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;

        lr.enabled = showRange; 
        DrawCircle(); 
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
     
    }

    void Update()
    {
        if (showRange)
        {
            DrawCircle();
            if (!lr.enabled) lr.enabled = true;
        }
        else
        {
            if (lr.enabled) lr.enabled = false;
        }
         DealDamage();
    }

    void DrawCircle()
    {
        float angle = 0f;
        Vector3 center = transform.position;

        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius + center.x;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius + center.y;
            lr.SetPosition(i, new Vector3(x, y, 0));
            angle += 360f / segments;
        }
    }

    void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Destroy(this.gameObject); // Destroy poison after hitting player
              Debug.Log($"Player hit by poison! Stunned for {stunTime} seconds.");
            }
        }
        
    }
}
