using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectateAnimations : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private float animTime;
    private int randAnim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("PlayRandAnimation");
    }


    IEnumerator PlayRandAnimation()
    {

        animTime = Random.Range(3,16);
        randAnim = Random.Range(0,3);

        if (randAnim == 0)
        {
            animTime = 1;
        }

        anim.SetInteger("randAnim", randAnim);

        yield return new WaitForSeconds(animTime);

        StartCoroutine(PlayRandAnimation());
    }
}
