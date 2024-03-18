using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant : MonoBehaviour
{
    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_WIN = "win";
    public const string ANIM_DEAD = "dead";
    public const string ANIM_DANCE = "dance";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_ULTI = "ulti";

    public const string TAG_CHARACTER = "Character";
    public const string TAG_BLOCK = "Block";

    public const float DELAY_TIME_THROW = 0.5f;
    public const float ATTACK_RANGE = 3.73f;
}

public enum ParticleType
{
    OnHit,
    LevelUp,
    Revive,
    Shield,
}

public enum PoolType
{
    None,

    Bot,

    W_Boomerang_1,
    W_Boomerang_2,
    W_Boomerang_3,
    W_Candy_1,
    W_Candy_2,
    W_Candy_3,
    W_Hammer_1,
    W_Hammer_2,
    W_Hammer_3,

    B_Boomerang_1,
    B_Boomerang_2,
    B_Boomerang_3,
    B_Candy_1,
    B_Candy_2,
    B_Candy_3,
    B_Hammer_1,
    B_Hammer_2,
    B_Hammer_3,

    SKIN_Normal,
    SKIN_Devil,
    SKIN_Angle,
    SKIN_Witch,
    SKIN_Deadpool,
    SKIN_Thor,

    HAT_1,
    HAT_2,
    HAT_3,
    HAT_4,
    HAT_5,
    HAT_6,
    HAT_7,
    HAT_8,
    HAT_9,

    ACC_Book,
    ACC_Captain,
    ACC_Shield,

    TargetIndicator,

    Player,

    VFX_Revive,
}

public enum WeaponType
{
    W_Boomerang_1 = PoolType.W_Boomerang_1,
    W_Boomerang_2 = PoolType.W_Boomerang_2,
    W_Boomerang_3 = PoolType.W_Boomerang_3,
    W_Candy_1 = PoolType.W_Candy_1,
    W_Candy_2 = PoolType.W_Candy_2,
    W_Candy_3 = PoolType.W_Candy_3,
    W_Hammer_1 = PoolType.W_Hammer_1,
    W_Hammer_2 = PoolType.W_Hammer_2,
    W_Hammer_3 = PoolType.W_Hammer_3,
}

public enum BulletType
{
    B_Boomerang_1 = PoolType.B_Boomerang_1,
    B_Boomerang_2 = PoolType.B_Boomerang_2,
    B_Boomerang_3 = PoolType.B_Boomerang_3,
    B_Candy_1 = PoolType.B_Candy_1,
    B_Candy_2 = PoolType.B_Candy_2,
    B_Candy_3 = PoolType.B_Candy_3,
    B_Hammer_1 = PoolType.B_Hammer_1,
    B_Hammer_2 = PoolType.B_Hammer_2,
    B_Hammer_3 = PoolType.B_Hammer_3,
}

public enum HatType
{
    HAT_None = 0,
    HAT_1 = PoolType.HAT_1,
    HAT_2 = PoolType.HAT_2,
    HAT_3 = PoolType.HAT_3,
    HAT_4 = PoolType.HAT_4,
    HAT_5 = PoolType.HAT_5,
    HAT_6 = PoolType.HAT_6,
    HAT_7 = PoolType.HAT_7,
    HAT_8 = PoolType.HAT_8,
    HAT_9 = PoolType.HAT_9,
}

public enum SkinType
{
    SKIN_Normal = PoolType.SKIN_Normal,
    SKIN_Devil = PoolType.SKIN_Devil,
    SKIN_Angle = PoolType.SKIN_Angle,
    SKIN_Witch = PoolType.SKIN_Witch,
    SKIN_Deadpool = PoolType.SKIN_Deadpool,
    SKIN_Thor = PoolType.SKIN_Thor,
}

public enum AccessoryType
{
    ACC_None = 0,
    ACC_Book = PoolType.ACC_Book,
    ACC_CaptainShield = PoolType.ACC_Captain,
    ACC_Shield = PoolType.ACC_Shield,
}

public enum PantType
{
    Pant_1,
    Pant_2,
    Pant_3,
    Pant_4,
    Pant_5,
    Pant_6,
    Pant_7,
    Pant_8,
    Pant_9,
}
