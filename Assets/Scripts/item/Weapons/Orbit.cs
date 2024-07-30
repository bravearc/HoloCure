using UnityEngine;

public class Orbit : WeaponRotation
{
    Vector2 size = new Vector2(0.8f, 0.8f);

    protected override void WeaponSetComponenet(Attack attack)
    {
        _cursor = transform;
        attack.SetAttackComponent(false, size, Vector2.one, Vector2.zero, true);

        ParticleSystem particleSystem = attack.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startLifetime = 0.15f;
        mainModule.startSpeed = 0;
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
        renderer.material = Manager.Asset.LoadMaterial("mat_Orbi_0");
    }

    
}
