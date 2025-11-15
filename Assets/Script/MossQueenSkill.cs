using UnityEngine;

public class MossQueenSkill : MonoBehaviour
{
     public GameObject poisonPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    public Animator anim;
    public float spawnDelay = 0.5f; // thời gian trễ sau khi chạy anim

    private float timer;
    public Transform target;


    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.M))
        {
            InvokeSkill();
        }
    

    }
    public void InvokeSkill()
    {
        if (timer >= spawnInterval)
        {
            SkillAttack();
            timer = 0f;
        }
    }
    public void SkillAttack()
    {
        anim.SetTrigger("Skill");
        anim.SetBool("Action", true);

        // Spawn đá sau spawnDelay
        Invoke(nameof(SpawnStone), spawnDelay);

        // Tắt Action sau 1 giây
        Invoke(nameof(StopAction), 1f);
    }

    // Hàm tắt Action
    private void StopAction()
    {
        anim.SetBool("Action", false);
    }

    [System.Obsolete]
    void SpawnStone()
    {
        if (target == null) return;

        Vector2 start = spawnPoint ? (Vector2)spawnPoint.position : (Vector2)transform.position;
        Vector2 end = target.position;

        GameObject stone = Instantiate(poisonPrefab, start, Quaternion.identity);
        Rigidbody2D rb2d = stone.GetComponent<Rigidbody2D>();
        if (rb2d == null) return;

        ThrowStone2D(rb2d, start, end, 3f); // 3f là chiều cao đỉnh parabola
    }
    [System.Obsolete]
    void ThrowStone2D(Rigidbody2D rb, Vector2 start, Vector2 end, float height)
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        Vector2 toTarget = end - start;

        // Đảm bảo height hợp lý, tránh sqrt âm
        float apexHeight = Mathf.Max(height, toTarget.y + 0.1f);

        float time = Mathf.Sqrt(2 * apexHeight / gravity) + Mathf.Sqrt(2 * Mathf.Max(apexHeight - toTarget.y, 0.1f) / gravity);

        if (time <= 0) time = 0.1f;

        // Vận tốc ném
        Vector2 velocityY = Vector2.up * Mathf.Sqrt(2 * gravity * apexHeight);
        Vector2 velocityXZ = toTarget / time;

        Vector2 velocity = velocityXZ + velocityY;

        rb.velocity = velocity;
    }

}
