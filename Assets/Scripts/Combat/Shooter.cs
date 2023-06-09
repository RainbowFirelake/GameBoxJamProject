﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooter : MonoBehaviour
{
    public event Action<int> OnAmmoCountUpdate;

    [SerializeField] private Transform gun;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Weapons currentWeapon = null;
    [SerializeField] private Weapons defaultWeapon = null;
    [SerializeField] Transform rightHandTransform = null;
    [SerializeField] Transform leftHandTransform = null;
    [SerializeField] private AudioSource _source;
    [SerializeField] private SideManager _sideManager;

    private float timeAfterLastShoot = Mathf.Infinity;
    private float timeOnOneShot;
    private int _currentAmmoCount;

    private void Start() 
    {
        SetupDefaultWeapon();
    }

    private void Update() 
    {
        timeAfterLastShoot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (timeAfterLastShoot > timeOnOneShot && _currentAmmoCount > 0)
        {
            currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, 
                rightHandTransform.rotation, _sideManager.GetSide());
            var clip = currentWeapon.GetSoundPlayer().GetRandomSound();
            if (_source != null)
            {
                _source.PlayOneShot(clip);
            }
            timeAfterLastShoot = 0;
            _currentAmmoCount--;
            OnAmmoCountUpdate?.Invoke(_currentAmmoCount);
        }
    }

    public void SetAimDirection(Vector3 point)
    {
        leftHandTransform.LookAt(point);
        rightHandTransform.LookAt(point);
    }

    public void EquipWeapon(Weapons weapon)
    {
        currentWeapon = weapon;
        AttachWeapon(weapon);
    }

    private void AttachWeapon(Weapons weapon)
    {
        InitializeRateOfFire();
        _currentAmmoCount = weapon.GetAmmoCount();
        OnAmmoCountUpdate?.Invoke(_currentAmmoCount);
        currentWeapon.Equip(rightHandTransform, leftHandTransform);
    }

    private Weapons SetupDefaultWeapon()
    {
        AttachWeapon(defaultWeapon);
        return defaultWeapon;
    }

    private void InitializeRateOfFire()
    {
        float x = (float)currentWeapon.GetRateOfFire() / 60;
        timeOnOneShot = 1 / x;
        timeAfterLastShoot = Mathf.Infinity;
    }
}
