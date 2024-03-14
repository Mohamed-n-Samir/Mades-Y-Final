using UnityEngine;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    private Transform WeaponSpriteTransform;
    private SpriteRenderer baseSpriteRenderer;
    private SpriteRenderer weaponSpriteRenderer;

    private int currentWeaponSpriteIndex;


    protected override void HandleEnter()
    {
        base.HandleEnter();

        currentWeaponSpriteIndex = 0;
    }

    protected override void Start()
    {
        base.Start();
        WeaponSpriteTransform = transform.Find("WeaponSprite").GetComponent<Transform>();

        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

        data = weapon.Data.GetData<WeaponSpriteData>();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
    }

    private void HandleBaseSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        weaponSpriteRenderer.sprite = currentAttackData.Sprites[currentWeaponSpriteIndex];
        int x = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Horizontal"));
        int y = (int)Mathf.Round(weapon.BaseAnimator.GetFloat("Vertical"));

        weaponSpriteRenderer.flipX = false;
        weaponSpriteRenderer.flipY = false;



        if (x == 1 && y == 0)
        {
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x == -1 && y == 0)
        {
            weaponSpriteRenderer.flipX = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x == 1 && y == 1)
        {
            weaponSpriteRenderer.flipY = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 45);
        }
        else if (x == -1 && y == -1)
        {
            weaponSpriteRenderer.flipY = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 225);
        }
        else if (x == -1 && y == 1)
        {
            weaponSpriteRenderer.flipY = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 135);
        }
        else if (x == 1 && y == -1)
        {
            weaponSpriteRenderer.flipY = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 315);
        }
        else if (x == 0 && y == 1)
        {
            weaponSpriteRenderer.flipY = true;
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, 90);
        }

        else if (x == 0 && y == -1)
        {
            WeaponSpriteTransform.localRotation = Quaternion.Euler(0, 0, -90);
        }

        currentWeaponSpriteIndex++;
    }

}