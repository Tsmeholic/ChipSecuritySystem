using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    internal class ChipSorter
    {
        internal Dictionary<Color, List<ColorChip>> _chipStartColors { get; set; }

        internal ChipSorter()
        {
            _chipStartColors = new Dictionary<Color, List<ColorChip>>();
        }

        internal void SortChips(List<ColorChip> chips)
        {
            foreach (Color color in Enum.GetValues(typeof(Color)))
            {
                _chipStartColors.Add(color, new List<ColorChip>());
            }
            foreach (var chip in chips)
            {
                _chipStartColors[chip.StartColor].Add(chip);
            }
        }
    }
}
