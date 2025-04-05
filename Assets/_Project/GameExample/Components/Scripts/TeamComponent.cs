using UnityEngine;

namespace ShootEmUp
{
    public sealed class TeamComponent
    {
        public Team CombatTeam => combatTeam;
        private Team combatTeam;

        public void Init(Team combatTeam)
        {
            this.combatTeam = combatTeam;
        }
    }

    public enum Team
    {
        NONE = 0,
        BLUE = 1,
        RED = 2
    }
}