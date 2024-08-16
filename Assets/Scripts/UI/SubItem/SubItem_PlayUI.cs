using System;
using System.Diagnostics;
using UnityEngine;
public class SubItem_PlayUI : UI_SubItem
{
    #region enum
    protected enum Sliders
    {
        HpSlider,
        SpecialSlider
    }
    protected enum Images
    {
        CharacterImage,
        SpecialImage,
        ExperiencePointsImage
    }
    protected enum Texts
    {
        ElapsedTimeText,
        GoldText,
        EnemyCountText,
        HpText,
        MaxHpText
    }
    #endregion

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindText(typeof(Texts));
        BindSlider(typeof(Sliders));

        BindModelEvent(Manager.Game.PlayTimeSecond, UpdateUI_Timer, this);
        BindModelEvent(Manager.Game.EnemyCount, UpdateUI_EnemyCount, this);
        BindModelEvent(Manager.Game.GoldCount, UpdateUI_GoldCount, this);
        BindModelEvent(Manager.Game.SpecialTimer, UpdateUI_SpecialSlider, this);
        BindModelEvent(Manager.Game.Character.MaxHp, UpdateUI_MaxHp, this);
        BindModelEvent(Manager.Game.Character.Hp, UpdateUI_Hp, this);
        BindModelEvent(Manager.Game.Character.CurrentExp, UpdateUI_ExperiencePointsImage, this);
        Set_UIImage();
    }
    private void UpdateUI_ExperiencePointsImage(float experiencePoints)
    {
        GetImage((int)Images.ExperiencePointsImage).fillAmount = experiencePoints;
    }

    private void Set_UIImage()
    {
        string icon = Manager.Game.GetCharacterData().Sprite;

        GetImage((int)Images.CharacterImage).sprite =
            Manager.Asset.LoadSprite($"spr_{icon}Portrait_0");
        //GetImage((int)Images.SpecialImage).sprite =
        //    Manager.Asset.LoadSprite($"spr_{icon}_Special_0");
        GetImage((int)Images.SpecialImage).sprite =
            Manager.Asset.LoadSprite($"spr_{icon}Portrait_0");
    }
    private void UpdateUI_MaxHp(int maxHp)
    {
        string text = string.Concat("/ ", maxHp.ToString());
        GetText((int)Texts.MaxHpText).text = text;
        GetSlider((int)Sliders.HpSlider).maxValue = maxHp;
    }
    private void UpdateUI_Hp(int hp)
    {
        GetText((int)Texts.HpText).text = hp.ToString();
        GetSlider((int)Sliders.HpSlider).value = hp;
    }
    private void UpdateUI_SpecialSlider(float SpesialTimer)
    {
        GetSlider((int)Sliders.SpecialSlider).value = SpesialTimer;
    }
    private void UpdateUI_GoldCount(int goldCount)
    {
        GetText((int)Texts.GoldText).text = goldCount.ToString();
    }
    private void UpdateUI_EnemyCount(int enemyCount)
    {
        GetText((int)Texts.EnemyCountText).text = enemyCount.ToString();
    }
    private void UpdateUI_Timer(int elapsedTime)
    {
        string minute = Manager.Game.PlayTimeMinute.Value.ToString("D2");
        string second = elapsedTime.ToString("D2");
        string time = $"{minute} : {second}";
        GetText((int)Texts.ElapsedTimeText).text = time;
    }


}
