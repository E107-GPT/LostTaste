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
}
