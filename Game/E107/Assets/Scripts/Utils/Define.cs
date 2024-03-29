using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{

    public enum Sound
    {
        BGM,
        Effect,
        MaxCount
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        Camp,
        Dungeon
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press,
        Click
    }
    public enum CameraMode
    {
        QuarterVeiw
    }

    public enum  UnitType
    {
        Player = 0,
        Mushroom,
        DrillDuck,
        TurtleSlime,
        ToxicFlower,
        Crab,
        Fishman,
        NagaWizard,
        Demon,
        Salamander,
        Specter,
        Skeleton,
        Crocodile,
        IceKing,
        MonsterKing,
    }

    public enum Effect
    {
        NormalAttackEffect,
        BubbleWandSkillEffect,
        DrillDuckBeforeEffect,
        DrillDuckSlideEffect,
        DrillDuckAttackEffect,
        StrongSwingEffect,
        GalaxyZzzSkillEffect,
        WarriorClassSkillEffect,
        SemUmbrellaSkillEffect,
        BoomerangSkillEffect,
        ToxicFlowerMissileEffect,
        SalamanderFlameEffect,
        NagaWizardLightningEffect,
        DemonFireballEffect,
        CrabAttackEffect,
        FishmanAttackEffect,
        SkeletonAttackEffect,
        SpecterAttackEffect,
        CrocodileSwordEffect,
        CrocodileAttackEffect,
        CrocodileSwordTail,
        IceKingCleaveEffect,
        IceKingSpikeEffect,
        // KingHitDownAfterEffect,
        KingHitDownEndEffect,
        KingHitDownStartEffect,
        KingSlashLurkerEffect,
        KingSlashStartEffect,
        KingStabChargeEffect,
        KingStabEffect,
        KingJumpStartEffect,
        KingJumpAirEffect,
        KingJumpEndEffect,
        MushroomAttackEffect,
        TurtleSlimeAttackEffect,
        MaxCount
    }

    public enum NPCType
    {
        Normal,
    }

    public enum SkillType
    {
        None,
        LeftSkill,
        RightSkill,
        ClassSkill
    }

    public enum ClassType
    {
        None,
        Warrior
    }

    public enum Weapons
    {
        _0000_Fist,
        _0001_Cucumber,
        _0004_Boomerang,
        _0008_SemUmbrella,
        _0012_HeroSword,
        _0014_GalaxyZzz,
        _0019_FryingPan,
        _0020_SixTimesBibimbap,
        _0021_FiveTimesBibimbap,
        _0022_FourTimesBibimbap,
        _0023_ThreeTimesBibimbap,
        _0024_TwoTimesBibimbap,
        _0025_Bibimbap,
        _0027_Log,
        _0028_BubbleWand,
        _0029_RareSteak,
        _0031_MiniDrill,
        _0035_BoredApple,
        _0039_GmHand,
        _0041_CrocodileSword,
        _0042_IcePearlStaff,
        _0043_NoEnterSign,
    }
}
