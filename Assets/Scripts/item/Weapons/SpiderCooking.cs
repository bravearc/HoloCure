using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class SpiderCooking : WeaponRanged
{
    Vector2 size;
    Vector2 colSize = new Vector2(108, 108);
    IDisposable _disposable;
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, false, size, colSize, Vector2.zero, true);
        SpriteRenderer sprite = attack.GetSprite();
        sprite.color = new Color(1f, 1f, 1f, 120f / 255f);

        ParticleSystem particleSystem = attack.GetComponent<ParticleSystem>();
        particleSystem.Play();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.loop = true;
        mainModule.startDelay = 0;
        mainModule.gravityModifier = -0.2f;
        mainModule.startLifetime = 0.5f;
        mainModule.startSpeed = 2;
        mainModule.startSize = 5f;
        mainModule.startColor = new Color(121 / 255f, 4 / 255f, 255 / 255f);
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        mainModule.simulationSpeed = 1;

        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 100;

        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.shapeType = ParticleSystemShapeType.Cone;
        shapeModule.radius = 53f;
        shapeModule.angle = 0;

        ParticleSystem.ForceOverLifetimeModule forceOverLifetimeModule = particleSystem.forceOverLifetime;
        forceOverLifetimeModule.enabled = true;
        forceOverLifetimeModule.y = 0.2f;

        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.enabled = true;
        renderer.material = Manager.Asset.LoadMaterial("mat_SakuraGohei_0");
        renderer.sortingOrder = 2;

        _disposable = attack.OnDisableAsObservable().Subscribe(_ => 
        {
            sprite.color = new Color(1, 1, 1, 1);   
            _disposable?.Dispose();
            _disposable = null;
        });
    }
}
