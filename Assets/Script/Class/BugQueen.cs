using UnityEngine;

public class BugQueen : MonoBehaviour,ISkill
{
    public float spawnInterval = 3f;

    public Animator anim;
    public float spawnDelay = 0.5f; 

    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void Execute()
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

        // Tắt Action sau 1 giây
        Invoke(nameof(StopAction), 1f);
    }

    // Hàm tắt Action
    private void StopAction()
    {
        anim.SetBool("Action", false);
    }

}
