using MoreMountains.Feedbacks;
using UnityEngine;

public class DrKChase : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private MMF_Player footsteps;
    [SerializeField] private float footStepTime;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play("FastWalk");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            footsteps.PlayFeedbacks();
            timer = footStepTime;
        }
            
        rigidbody.linearVelocity = (transform.forward * speed);
    }
}
