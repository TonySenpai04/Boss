using NUnit.Framework;
using UnityEngine;


public class GolemSkill : MonoBehaviour,ISkill
{
    public GameObject stonePrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;

    public Animator anim;
    public float spawnDelay = 0.5f; // thời gian trễ sau khi chạy anim

    private float timer;
    public Transform target;
    public ISpawner spawner;

    void Start()
    {
      spawner = new Spawner(stonePrefab, spawnPoint);

    }
    void Update()
    {
        timer += Time.deltaTime;

    }
    public void SkillAttack()
    {
        anim.SetTrigger("Skill");
        anim.SetBool("Action", true);

        Invoke(nameof(SpawnStone), spawnDelay);

        Invoke(nameof(StopAction), 1f);
    }

    private void StopAction()
    {
        anim.SetBool("Action", false);
    }

    [System.Obsolete]
    void SpawnStone()
    {
        spawner.Spawn(target, spawnInterval);

    }

    public void Execute()
    {
       if (timer >= spawnInterval)
        {
            SkillAttack();
            timer = 0f;
        }
    }

  

}  