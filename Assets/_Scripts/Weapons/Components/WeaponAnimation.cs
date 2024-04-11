using System.Collections;
using UnityEngine;

public class WeaponAnimation : WeaponComponent<WeaponAnimationData, AttackAnimations>
{
    private SpriteRenderer baseSpriteRenderer;
    public Animator WeaponAnimator { get; private set; }
    private bool isPlaying = false;


    protected override void HandleEnter()
    {
        base.HandleEnter();
        PlayAnimation();
    }

    protected override void HandleExit()
    {
        base.HandleExit();
        StopAnimation();
    }

    protected override void Start()
    {
        base.Start();

        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        WeaponAnimator = weapon.WeaponSpriteGameObject.GetComponent<Animator>();

        data = weapon.Data.GetData<WeaponAnimationData>();
    }


    private void PlayAnimation()
    {
        if (isAttackActive)
        {
            isPlaying = true;

            int x = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Horizontal"));
            int y = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Vertical"));

            if (x == -1 && y == 0)
            {
                WeaponAnimator.CrossFade(currentAttackData.Animations[0].name, 0, 0);
                // Debug.Log(currentAttackData.Animations[0].name);
            }
            else if (x == 0 && y == 1)
            {
                WeaponAnimator.CrossFade(currentAttackData.Animations[1].name, 0, 0);
                // Debug.Log(currentAttackData.Animations[0].name);
            }
            else if (x == 1 && y == 0)
            {
                WeaponAnimator.CrossFade(currentAttackData.Animations[2].name, 0, 0);
                // Debug.Log(currentAttackData.Animations[0].name);
            }
            else if (x == 0 && y == -1)
            {
                WeaponAnimator.CrossFade(currentAttackData.Animations[3].name, 0, 0);
                // Debug.Log(currentAttackData.Animations[0].name);
            }

        }
    }

    private void StopAnimation()
    {
        if (isPlaying)
        {
            WeaponAnimator.CrossFade("Empty", 0, 0);
            // Debug.Log("animation stopped");
        }
    }


}
















// public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
// {
//     private Transform WeaponSpriteTransform;
//     private SpriteRenderer baseSpriteRenderer;
//     // private SpriteRenderer weaponSpriteRenderer;
//     public Animator WeaponAnimator { get; private set; }



//     private int currentWeaponSpriteIndex;


//     protected override void HandleEnter()
//     {
//         base.HandleEnter();

//         currentWeaponSpriteIndex = 0;
//     }

//     protected override void Start()
//     {
//         base.Start();
//         WeaponSpriteTransform = transform.Find("WeaponSprite").GetComponent<Transform>();

//         baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
//         // weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
//         WeaponAnimator = weapon.WeaponSpriteGameObject.GetComponent<Animator>();

//         data = weapon.Data.GetData<WeaponSpriteData>();
//         baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
//     }


//     protected override void OnDestroy()
//     {
//         base.OnDestroy();
//         baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
//     }



//     private void HandleBaseSpriteChange(SpriteRenderer sr)
//     {
//         if (!isAttackActive)
//         {
//             weaponSpriteRenderer.sprite = null;
//             return;
//         }

//         weaponSpriteRenderer.sprite = currentAttackData.Sprites[currentWeaponSpriteIndex];
//         int x = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Horizontal"));
//         int y = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Vertical"));

//         weaponSpriteRenderer.flipX = false;
//         weaponSpriteRenderer.flipY = false;



//         if (x == 1 && y == 0)
//         {
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
//         }
//         else if (x == -1 && y == 0)
//         {
//             weaponSpriteRenderer.flipX = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
//         }
//         else if (x == 1 && y == 1)
//         {
//             weaponSpriteRenderer.flipY = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 45);
//         }
//         else if (x == -1 && y == -1)
//         {
//             weaponSpriteRenderer.flipY = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 225);
//         }
//         else if (x == -1 && y == 1)
//         {
//             weaponSpriteRenderer.flipY = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 135);
//         }
//         else if (x == 1 && y == -1)
//         {
//             weaponSpriteRenderer.flipY = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 315);
//         }
//         else if (x == 0 && y == 1)
//         {
//             weaponSpriteRenderer.flipY = true;
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 90);
//         }

//         else if (x == 0 && y == -1)
//         {
//             WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, -90);
//         }

//         currentWeaponSpriteIndex++;
//     }

// }