using UnityEngine;

public class Bright : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(33, 33);
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, true, size, colSize, Vector2.zero, true);
        
        ParticleSystem particleSystem = attack.GetComponent<ParticleSystem>();
        particleSystem.Stop();

        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startLifetime = 0.15f;
        mainModule.startSpeed = 0;
        mainModule.startSize = 30;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        mainModule.maxParticles = 100;

        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 20;

        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.enabled = false;

        ParticleSystem.ColorOverLifetimeModule colorOverLifetime = particleSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        colorOverLifetime.color = new Color(255, 254, 0);

        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = Manager.Asset.LoadMaterial("mat_Bright_0");
        ParticleSystem.RotationBySpeedModule rotationBySpeedModule = particleSystem.rotationBySpeed;
        rotationBySpeedModule.enabled = true;
        rotationBySpeedModule.zMultiplier = 15;

        particleSystem.Play();
    }
}
