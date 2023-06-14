using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    internal class PathFinder
    {
        ChipSorter _sorter { get; set; }
        internal List<ColorChip> _longestPath { get; set; }
        private Dictionary<Color, List<ColorChip>> _remainingChips { get; set; }
        private List<ColorChip> _blueChips { get; set; }
        private int _lastGreenChip = 0;

        internal PathFinder(ChipSorter sorter)
        {
            _sorter = sorter;
            _longestPath = new List<ColorChip>();
        }

        internal void ResetRemainingChips()
        {
            _remainingChips = _sorter._chipStartColors;
        }

        internal void FindPath()
        {
            List<ColorChip> temporaryPath = new List<ColorChip>();

            _blueChips = _sorter._chipStartColors[Color.Blue];

            //Start with each blue chip
            for (int i = 0; i < _blueChips.Count; i++)
            {
                ResetRemainingChips();

                temporaryPath.Add(_remainingChips[Color.Blue][i]);
                _remainingChips[Color.Blue].RemoveAt(i);
                temporaryPath = LinkChips(temporaryPath);

                if (_lastGreenChip > _longestPath.Count)
                {
                    _longestPath = temporaryPath.GetRange(0, _lastGreenChip);
                }
            }
        }

        internal List<ColorChip> LinkChips(List<ColorChip> temporaryPath) 
        {
            Color endColor = temporaryPath[temporaryPath.Count - 1].EndColor;
            if (endColor == Color.Green)
            {
                _lastGreenChip = temporaryPath.Count;
            }

            // If all chips have been used, exit recursion
            if (!_remainingChips.Select(c => c.Value.Count > 0).First())
            {
                return temporaryPath;
            }

            // If more tokens of endColor are present, add to path. Else remove last token and try again.
            if (_remainingChips[endColor].Count() > 0)
            {
                temporaryPath.Add(_remainingChips[endColor][0]);
                _remainingChips[endColor].RemoveAt(0);
                LinkChips(temporaryPath);
            }
            else
            {
                temporaryPath.RemoveAt(temporaryPath.Count() - 1);
                LinkChips(temporaryPath);
            }
            return temporaryPath;
        }

        internal bool ValidatePath()
        {
            if (_longestPath[0].StartColor == Color.Blue && _longestPath[_longestPath.Count() - 1].EndColor == Color.Green)
            {
                return true;
            }
            return false;
        }
    }
}
