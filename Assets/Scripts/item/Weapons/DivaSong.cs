using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DivaSong : WeaponMultishot
{
    Vector2 size;
    Vector2 colSize = new Vector2(33, 33);
    float shakeY = 0.2f;
    float shakeDuration = 0.2f;

    IEnumerator _moveCo;
    IDisposable _disposable;

    protected override void WeaponSetComponent(Attack attack)
    {
        size = new Vector2(_weaponData.Size, _weaponData.Size);
        attack.SetAttackComponent(false, true, size, colSize, Vector2.zero, true);
        
        ParticleSystem particleSystem = attack.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        mainModule.startColor = new Color(234f / 255f, 106f / 255f, 87f / 255f);
        mainModule.startLifetime = 0.15f;
        mainModule.startSize = 35;
        mainModule.startSpeed = 0;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
        mainModule.maxParticles = 100;

        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.rateOverTime = 20;

        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.enabled = false;

        ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        renderer.material = Manager.Asset.LoadMaterial("mat_DivaSong_0");
        renderer.flip = _cursor.position.x > _character.transform.position.x ? Vector2.zero : Vector2.right;

        _disposable = attack.OnDisableAsObservable().Subscribe(_ =>
        {
            StopCoroutine(_moveCo);
            _disposable?.Dispose();
            _moveCo = null;
            _disposable = null;
        });
        _moveCo = Move(attack);
        StartCoroutine(_moveCo);
    }

    IEnumerator Move(Attack attack) 
    {
        Rigidbody2D rb = attack.GetRigid();

        Vector2 baseDirection = (_cursor.position - _character.transform.position).normalized;
        baseDirection = Quaternion.Euler(0, 0, _angle) * baseDirection;

        while (true)
        {
            float newY = Mathf.Sin(Time.time * Mathf.PI / shakeDuration) * shakeY;
            Vector2 shakeOffset = new Vector2(0, newY);

            Vector2 finalDirection = baseDirection * _weaponData.Speed * Time.fixedDeltaTime;
            finalDirection.y += shakeOffset.y;

            rb.MovePosition(rb.position + finalDirection);


            //yield return null;
            yield return new WaitForFixedUpdate();
        }
        
    }
}
