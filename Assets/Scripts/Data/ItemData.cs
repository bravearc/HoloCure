
public enum ItemID
{
    Weapon = 1000,
    SpiderCooking,
    EliteLavaBucket,
    CEOsTears,
    HoloBomb,
    WamyWater,
    Sausage,
    PsychoAxe,
    BL_Book,
    CuttingBoard,
    FanBeam,
    PlugTypeAsacoco,
    Glowstick,
    IdloSong,
    BounceBall,
    ENsCurse,
    X_Potato,
    PistolShot = 1101,
    TridentThrust,
    SummonTentacle,
    PhoenixSword,
    ScytheSwing,
    PlayDice,
    ClockHands,
    BirdFeather = 1109,
    Orbit,
    FoxTail = 1112,
    TarotCards,
    Orayo = 1115,
    Bright,
    DivaSong,
    HighSpecPistol,
    AxeSwing,
    RedHeart =1121,
    KapuKapu,
    BaseballPitch = 1125,
    LoveNeedle,
    DualKatana,
    Equipment = 2000,
    NinjaHeadband,
    HopeSoda,
    Sake,
    CandyKingdomSweets,
    Limiter,
    BodyPillow,
    DevilHat,
    InjectionTypeAsacoco,
    Halu,
    FocusShades,
    KnightlyMilk,
    NursesHorn,
    Headphones,
    GorillasPaw,
    UberSheep,
    IdolCostume,
    EnergyDrink,
    StudyGlasses,
    JustBandage,
    Drop = 3000,
    Anvil,
    ExperiencePoints,
    HoloCoin,
    Stats = 3500,
    MaxHPUp,
    ATKUp,
    SPDUp,
    CRTUp,
    PickUpRangeUp,
    HasteUp,
    MaxItem
}
public enum ItemType
{
    StartingWeapon,
    Weapon,
    Equipment,
    Stat
}
public enum WeaponType
{
    None,
    Melee,
    Ranged,
    Multishot
}

public class EquipmentData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Explanation { get; set; }
}
public class WeaponData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public float Attack { get; set; }
    public float Quantity { get; set; }
    public float Speed { get; set; }
    public float Duration { get; set; }
    public float AttackRange { get; set; }
    public float AttackCycle { get; set; }
    public float Size { get; set; }
    public int Knockback { get; set; }
    public int MaxLevel { get; set; }
    public string Explanation { get; set; }
    public WeaponType WeaponType { get; set; }
    public string Animation { get; set; }
}
public class ItemData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public string IconImage { get; set; }
    public string Sound { get; set; }
}

public class StatsData
{
    public ItemID ID { get; set; }
    public string Name { get; set; }
    public string Explanation { get; set; }
}
