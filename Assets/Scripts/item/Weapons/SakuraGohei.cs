using System.Collections;
using UnityEngine;

public class SakuraGohei : WeaponMelee
{
    Vector2 size;
    Vector2 colSize = new Vector2(60, 60f);
    Vector2 offset = new Vector2(17, 2);
    bool isImpect;
    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(true, true, size, colSize, offset, true);
        isImpect = false;
        StartCoroutine(ImpectCo(attack));
    }

    IEnumerator ImpectCo(Attack attack)
    {

        float particleTime = 0;
        while (isImpect == false)
        {
            if(particleTime > attack.GetAniLength())
            {
                ParticleSystem particleSystem = attack.GetComponent<ParticleSystem>();
                particleSystem.Stop();
                
                ParticleSystem.MainModule mainModule = particleSystem.main;
                mainModule.loop = false;
                mainModule.duration = 0.2f;
                mainModule.startDelay = 0.2f;
                mainModule.startColor = new Color(106f / 255f, 39f / 255f, 16f / 255f);
                mainModule.startLifetime = 80;
                mainModule.startSpeed = 100f;
                mainModule.startSize = 10;
                mainModule.gravityModifier = 0.3f;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

                ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
                emissionModule.enabled = true;
                emissionModule.rateOverTime = 150f;

                ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
                shapeModule.enabled = true;
                shapeModule.shapeType = ParticleSystemShapeType.Sphere;
                shapeModule.angle = 0;
                shapeModule.position = new Vector2(30f, 0);
                shapeModule.scale = new Vector2(0.5f, 0.5f);

                var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
                renderer.enabled = true;
                renderer.material = Manager.Asset.LoadMaterial("mat_SakuraGohei_0");

                particleSystem.Play();
                isImpect = true;
            }
            particleTime += Time.time;
            yield return null;
        }

    }
}
