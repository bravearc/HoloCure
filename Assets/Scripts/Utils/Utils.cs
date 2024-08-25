using System;
using UnityEngine;


public class Utils : MonoBehaviour
{
    public static T ParseEnum<T>(string value, bool ignreCase = true)
    {
        return (T)Enum.Parse(typeof(T), value, ignreCase);
    }

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            return go.AddComponent<T>();
        }
        return component;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
        {
            if (go == null)
            {
                return null;
            }

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            Debug.Assert(transform != null);
            return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) == false && component.name != name) continue;
                    
                return component;
            }
        }
        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static bool IsWeapon(ItemID id)
    {
        if ((int)Define.ItemNumber.Weapon_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.StartingWeapon_End)
        {  
            return true; 
        }
        return false;
    }

    public static float GetRandomAngle()
    {
        return UnityEngine.Random.Range(-0.2f, 0.2f);
    }

    public static bool IsEquipment(ItemID id)
    {
        if((int)Define.ItemNumber.Equipment_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.Equipment_End)
        {
            return true;
        }
        return false;
    }

    public static string GetItemType(ItemID id)
    {
        if ((int)id < (int)Define.ItemNumber.StartingWeapon_End)
            return "Weapon";
        else if ((int)id < (int)Define.ItemNumber.StartingWeapon_End)
            return "Equipment";
        else if ((int)Define.ItemNumber.Stats_Start <= (int)id
            && (int)id < (int)Define.ItemNumber.Stats_End)
            return "Stats";
        else
            return "Drop";
    }

    public static void ResetParticle(GameObject go)
    {
        var particleSystem = go.GetComponent<ParticleSystem>();
        if (particleSystem == null)
            return;

        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var mainModule = particleSystem.main;
        mainModule.duration = 5f;
        mainModule.loop = true;
        mainModule.startColor = Color.white;
        mainModule.startLifetime = 1f;
        mainModule.startSpeed = 5f;
        mainModule.startSize = 1f;
        mainModule.gravityModifier = 0f;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.Local;

        var emissionModule = particleSystem.emission;
        emissionModule.enabled = true;
        emissionModule.rateOverTime = 10f;
        emissionModule.rateOverDistance = 0f;

        var shapeModule = particleSystem.shape;
        shapeModule.enabled = true;
        shapeModule.shapeType = ParticleSystemShapeType.Cone;
        shapeModule.scale = new Vector3(1f, 1f, 1f);

        var velocityOverLifetimeModule = particleSystem.velocityOverLifetime;
        velocityOverLifetimeModule.enabled = false;

        var limitVelocityOverLifetimeModule = particleSystem.limitVelocityOverLifetime;
        limitVelocityOverLifetimeModule.enabled = false;

        var forceOverLifetimeModule = particleSystem.forceOverLifetime;
        forceOverLifetimeModule.enabled = false;

        var colorOverLifetimeModule = particleSystem.colorOverLifetime;
        colorOverLifetimeModule.enabled = false;

        var colorBySpeedModule = particleSystem.colorBySpeed;
        colorBySpeedModule.enabled = false;

        var sizeOverLifetimeModule = particleSystem.sizeOverLifetime;
        sizeOverLifetimeModule.enabled = false;

        var sizeBySpeedModule = particleSystem.sizeBySpeed;
        sizeBySpeedModule.enabled = false;

        var rotationOverLifetimeModule = particleSystem.rotationOverLifetime;
        rotationOverLifetimeModule.enabled = false;

        var rotationBySpeedModule = particleSystem.rotationBySpeed;
        rotationBySpeedModule.enabled = false;

        var externalForcesModule = particleSystem.externalForces;
        externalForcesModule.enabled = false;

        var noiseModule = particleSystem.noise;
        noiseModule.enabled = false;

        var collisionModule = particleSystem.collision;
        collisionModule.enabled = false;

        var triggerModule = particleSystem.trigger;
        triggerModule.enabled = false;

        var subEmittersModule = particleSystem.subEmitters;
        subEmittersModule.enabled = false;

        var textureSheetAnimationModule = particleSystem.textureSheetAnimation;
        textureSheetAnimationModule.enabled = false;

        var lightsModule = particleSystem.lights;
        lightsModule.enabled = false;

        var trailsModule = particleSystem.trails;
        trailsModule.enabled = false;

        var customDataModule = particleSystem.customData;
        customDataModule.enabled = false;

        var renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.enabled = true;
        renderer.material = null;
    }

    public static void ResetRigidbody(Rigidbody2D rb)
    {
        rb.angularVelocity = 0f;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.drag = 0f;
        rb.angularDrag = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
    }
}
