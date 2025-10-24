using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;

    private static readonly int jump = Animator.StringToHash("Jump");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        playerMovement.OnPlayerMove += JumpAnim;
    }

    private void OnDisable()
    {
        playerMovement.OnPlayerMove -= JumpAnim;
    }

    private void JumpAnim()
    {
        animator.SetTrigger(jump);
    }
}
