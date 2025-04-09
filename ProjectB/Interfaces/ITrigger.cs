using ProjectB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Interfaces
{
    public interface ITrigger
    {
		bool IsTrigger(Player player);
        void OnTrigger(Player player);
    }
}
