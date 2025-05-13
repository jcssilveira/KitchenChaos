using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string IS_WALKING  = "IsWalking";
    private Animator animator;
    [SerializeField] Player player;

    // Start is called before the first frame update
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsMoving());
    }
}
