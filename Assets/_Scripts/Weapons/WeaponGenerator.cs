using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private WeaponDataSO data;
    [SerializeField] private Weapon weapon;

    private List<WeaponComponent> componentsAlreadyOnWeapon = new List<WeaponComponent>();

    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();

    private List<Type> componentDepecdencies = new List<Type>();

    private Animator weeaponAnimator;

    private void Start()
    {
        weeaponAnimator = GetComponentInChildren<Animator>();
        GenerateWeapon(data);
    }

    [ContextMenu("test Generate")]
    private void TestGeneration(){
        GenerateWeapon(data);
    }

    public void GenerateWeapon(WeaponDataSO data)
    {
        weapon.SetData(data);

        componentsAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDepecdencies.Clear();

        componentsAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

        componentDepecdencies = data.GetAllDependencies();

        foreach (var dependency in componentDepecdencies)
        {
            if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
            {
                continue;
            }

            var weaponComponent = componentsAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            weaponComponent.Init();

            componentsAddedToWeapon.Add(weaponComponent);
        }

        var componentsToRemove = componentsAlreadyOnWeapon.Except(componentsAddedToWeapon);

        foreach (var weaponComponent in componentsToRemove){
            Destroy(weaponComponent);
        }

        weeaponAnimator.runtimeAnimatorController = data.AnimatorController;

    }
}