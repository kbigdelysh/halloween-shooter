using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Data
{
    /// <summary>
    /// This class holds game data.
    /// </summary>
    [Serializable]
    public class ConfigData
    {        
        public string PumpkinTypeSpawn;
        public string Title;
        public string VersionNumber;
        public int GameSessionLenghtInSec;

    public  ConfigData()
        {
            PumpkinTypeSpawn = "Pumpkin_Orange";
            Title = "Pumpkin Shooter!";
            VersionNumber = "0.9";
            GameSessionLenghtInSec = 45;
        }
    }
}
