using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float _attack;
    public float _skill;
    private bool _isSkill;
    float _atCount;
    float _skCount;

    ISpecialSkill _specialSkill;

    void Awake()
    {
        _attack = 3f;
    }
    void FixedUpdate()
    {
        Attack();
        _skCount++;
        if(_skCount < _skill)
        {
            _isSkill = true;
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(1)) 
        {
            Skill();
        }
    }

    public void SkillSet(ISpecialSkill skill)
    {
        _specialSkill = skill;
    }
    public void Attack()
    {
        _atCount++;
        if(_atCount > _attack)
        {
            //공격 메서드
            _atCount = 0;
        }
    }

    public void Skill()
    {
        if(_isSkill)
        {
            _specialSkill.SpecialSkill();
            _isSkill = false;
            _skill = 0;
        }
    }


}
