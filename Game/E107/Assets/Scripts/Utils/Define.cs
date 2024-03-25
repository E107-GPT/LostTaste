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
        Slime,
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
    }

    public enum Effect
    {
        NormalAttackEffect,
        BubbleWandSkillEffect,
        DrillDuckBeforeEffect,
        DrillDuckSlideEffect,
        StrongSwingEffect,
        GalaxyZzzSkillEffect,
        WarriorClassSkillEffect,
        SemUmbrellaSkillEffect,
        BoomerangSkillEffect,
        ToxicFlowerMissileEffect,
        SalamanderFlameEffect,
        NagaWizardLightningEffect,
        DemonFireballEffect,
        CrocodileSwordEffect,
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
}
