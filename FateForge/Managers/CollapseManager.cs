using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge.Managers
{
    public static class CollapseManager
    {
        /// <summary>
        /// Obsolete.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="container"></param>
        /// <param name="initSize"></param>
        /// <param name="collapseTarget"></param>
        public static void Collapse(Button button, Control parent, Size _initSize)
        {
            if (parent.MinimumSize.Height != _initSize.Height)
            {
                button.Text = "-";
                parent.MinimumSize = _initSize;
                parent.Height = _initSize.Height;
            }
            else
            {
                button.Text = "o";
                parent.MinimumSize = new Size(parent.Width, 36);
                parent.Height = 36;
            }
        }

        /// <summary>
        /// Resizes childs in a neat and consistent matter.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="offset"></param>
        public static void ResizeChilds(Panel panel, int offset = 12)
        {
            int positionY = 0;
            foreach (Control c in panel.Controls)
            {
                if (c is IIndependentResize)
                {
                    ((IIndependentResize)c).IndependentResize();
                }
                else
                    c.Size = new Size(panel.Width - offset, c.MinimumSize.Height);
                c.Location = new Point(0, positionY - panel.VerticalScroll.Value);
                positionY += 2 + (c.Height);
            }
        }

        /// <summary>
        /// The Generic method for independent resizing. Calls ResizeChilds.
        /// </summary>
        /// <param name="interest">Control of interest.</param>
        /// <param name="dependents">Controls that determine size.</param>
        /// <param name="offset">For ResizeChilds.</param>
        public static void IndependentResize(Control interest, Panel dependents, int offset = 12)
        {
            int height = interest.MinimumSize.Height;

            ResizeChilds(dependents, offset);

            foreach (Control c in dependents.Controls)
                height += (c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height;

            interest.Size = new Size(interest.Parent.Width - 8, height);
        }

        public static void IndependentResize(Control interest, int offset, params Panel[] dependents)
        {
            int height = interest.MinimumSize.Height;

            //foreach(Panel panel in dependents)
            //    ResizeChilds(panel, offset);

            foreach (Panel panel in dependents)
                foreach (Control c in panel.Controls)
                    height += (c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height;

            interest.Size = new Size((interest.Parent != null) ? interest.Parent.Width - 8 : interest.Width, height);
        }

        /// <summary>
        /// The generic independent resize for controls that want to be confined to their child contents.
        /// </summary>
        /// <param name="interest"></param>
        /// <param name="panel"></param>
        public static void WrapIndependentSize(Control interest, Panel panel, int height=40)
        {
            CollapseManager.ResizeChilds(panel);
            foreach (Control c in panel.Controls)
                height += c.Size.Height + 12;
            interest.MinimumSize = new Size(interest.Width, height);
            interest.Size = interest.MinimumSize;
        }
    }
}
