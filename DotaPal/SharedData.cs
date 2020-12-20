using System.Collections.Generic;

namespace DotaPal
{
    public class SharedData
    {
        public Dictionary<Slots, int> Cooldown = new();
        public bool IsOnDarkSide = false;
    }
}