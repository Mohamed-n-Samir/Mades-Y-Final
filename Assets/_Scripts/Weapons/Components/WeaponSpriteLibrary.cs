using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class WeaponSpriteLibrary : WeaponComponent<WeaponSpriteLibraryData, AttackSpritesLibrary>
{
    public SpriteResolver spriteResolver;
    public SpriteLibrary spriteLibrary;
    public SpriteRenderer spriteRenderer;
    public Animator WeaponAnimator { get; private set; }
    private GameObject weaponBody;
    private bool isPlaying = false;


    protected override void HandleEnter()
    {
        base.HandleEnter();
        ChangeSprites();
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
        weaponBody = weapon.WeaponSpriteGameObject.transform.GetChild(0).gameObject;
        spriteResolver = weaponBody.GetComponent<SpriteResolver>();
        spriteLibrary = weaponBody.GetComponent<SpriteLibrary>();
        spriteRenderer = weaponBody.GetComponent<SpriteRenderer>();
        WeaponAnimator = weapon.WeaponSpriteGameObject.GetComponent<Animator>();

        data = weapon.Data.GetData<WeaponSpriteLibraryData>();
    }

    private void ChangeSprites()
    {
        // if (spriteLibrary != null && spriteLibrary.spriteLibraryAsset == null)
        // {
            // Change the sprite library asset
            spriteLibrary.spriteLibraryAsset = currentAttackData.SpriteLibraryAsset;
        // }
        // else
        // {
        //     Debug.LogError("spriteLibrary component not found!");
        // }

        // if (spriteResolver != null)
        // {
        //     // Change the sprite category
        //     Debug.Log(currentAttackData.SpriteLibraryAsset.GetCategoryNames().ToArray<string>()[CurrentAttackCounter]);
        //     spriteResolver.SetCategoryAndLabel(currentAttackData.SpriteLibraryAsset.GetCategoryNames().ToArray<string>()[CurrentAttackCounter], "0");
        // }
        // else
        // {
        //     Debug.LogError("SpriteResolver component not found!");
        // }
    }


    private void PlayAnimation()
    {
        if (isAttackActive)
        {
            isPlaying = true;

            // int x = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Horizontal"));
            // int y = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Vertical"));

            WeaponAnimator.CrossFade(currentAttackData.Animations.name, 0);

        }
    }

    private void StopAnimation()
    {
        if (isPlaying)
        {
            WeaponAnimator.CrossFade("Empty Body", 0);
            spriteLibrary.spriteLibraryAsset = null;
            spriteRenderer.sprite = null;
            Debug.Log("animation stopped");
        }

    }
}