using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative
{
    public enum SizeOfDungeon
    {
        Small = 16,
        Medium = 32,
        Large = 64
    }
    public enum TypeOfDungeon
    {
        Linear,
        Branching,
        Special
    }
    public enum RoomFlags
    {
        StartingRoom,
        BossRoom,
        SpecialRoom,
        PuzzleRoom
    }

    public enum Heading
    {
        North,
        East,
        South,
        West
    }
}
