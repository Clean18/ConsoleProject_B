using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Interfaces
{
    public interface IWildEncounter : ITrigger
    {
        int EncounterRate {  get; }
		int MinLevel { get; }
        int MaxLevel { get; }

    }
}
