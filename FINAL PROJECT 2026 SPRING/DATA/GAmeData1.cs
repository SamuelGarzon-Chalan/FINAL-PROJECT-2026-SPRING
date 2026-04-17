using FinalBattler.Characters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FinalBattler.Data
{
    public  class GameData1
    {
        public List <Fighter> TeamA { get; set; }
        public List <Fighter> TeamB { get; set; }
        public int NumberOfRounds { get; set; }

        public DateTime SavedData { get; set; }
    }
}
