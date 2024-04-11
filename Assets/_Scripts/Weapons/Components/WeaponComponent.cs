using UnityEngine;
using CoreSystem;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;

    // TODO : fix this finishing weapon data

    // protected WeaponsAnimationEventHandler EventHandler => weapon.EventHandler;
    protected WeaponsAnimationEventHandler eventHandler;

    public Player Player => weapon.Player;
    protected Core Core => weapon.Core;

    protected bool isAttackActive;

    public virtual void Init(){

    }

    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();

        eventHandler = GetComponentInChildren<WeaponsAnimationEventHandler>();
    }

    protected virtual void Start()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void HandleEnter()
    {
        isAttackActive = true;
        // Debug.Log("helawa");
    }

    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }

    protected virtual void OnDestroy()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }

}
public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
{
    protected T1 data;
    protected T2 currentAttackData;

    public override void Init()
    {
        base.Init();

        data = weapon.Data.GetData<T1>();
    }

    protected override void HandleEnter()
    {
        base.HandleEnter();
        // Debug.Log(data);

        currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
    }
}