using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKmanager : MonoBehaviour
{
    Animator animator;
    public bool ikActive = false;
    public Transform objTarget;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK()
    {
        if (animator)
        {
            if (ikActive)
            {
                if(objTarget != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(objTarget.position);
                }
            }
            else
            {
                animator.SetLookAtWeight(0);
            }
        }
    }
}
