using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge
{
    public interface IIndependentResize
    {
        /// <summary>
        /// This method resizes the control on its own terms.
        /// </summary>
        void IndependentResize();
        void IndependentCollapse(bool collapseState);
        int GetDesiredSize();
    }
}
