using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

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
        public static void Collapse(Button button, Control parent, Size _initSize, int collapseSize = 36)
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
                parent.MinimumSize = new Size(parent.Width, collapseSize);
                parent.Height = collapseSize;
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
                    c.Size = new Size(panel.Width - offset, ((c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height));
                c.Location = new Point(0, positionY - panel.VerticalScroll.Value);
                positionY += 2 + (c.Height);
            }
        }

        public static void ResizeChilds(ContainerControl container, int offset = 12)
        {
            int positionY = 0;
            foreach (Control c in container.Controls)
            {
                if (c is IIndependentResize)
                {
                    ((IIndependentResize)c).IndependentResize();
                }
                else
                    c.Size = new Size(container.Width - offset, ((c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height));
                c.Location = new Point(0, positionY - container.VerticalScroll.Value);
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

            interest.Size = new Size(((interest.Parent == null) ? 0 : interest.Parent.Width - 8), ((interest.Parent != null) ? ((interest.Parent.Height < height) ? interest.Parent.Height : height) : height));
        }
        
        public static void IndependentResize(Control interest, List<Panel> dependents, int offset = 12)
        {
            int height = interest.MinimumSize.Height;

            foreach(Panel p in dependents)
               ResizeChilds(p, offset);
            foreach(Panel p in dependents)
                foreach (Control c in p.Controls)
                    height += (c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height;

            interest.Size = new Size(((interest == null) ? 0 : interest.Parent.Width - 8), height);
        }

        public static List<Control> IndependentResize(Control interest, int offset=12)
        {
            List<Control> releventContainers = new List<Control>();
            foreach (Control c in interest.Controls)
                if (c is Panel)
                    releventContainers.Add(c);

            int height = interest.MinimumSize.Height;

            //foreach(Panel panel in dependents)
            //    ResizeChilds(panel, offset);

            foreach (Panel panel in releventContainers)
                foreach (Control c in panel.Controls)
                    height += (c.MinimumSize.Height == 0) ? c.Height : c.MinimumSize.Height;

            interest.Size = new Size((interest.Parent != null) ? interest.Parent.Width - 8 : interest.Width, height);

            return releventContainers;
        }

        /// <summary>
        /// The generic independent resize for controls that want to be confined to their child contents.
        /// </summary>
        /// <param name="interest"></param>
        /// <param name="panel"></param>
        public static void WrapIndependentSize(Control interest, Panel panel, int height = 40, int offset = 12)
        {
            int initHeight = height;

            CollapseManager.ResizeChilds(panel);
            foreach (Control c in panel.Controls)
                height += (c.Size.Height + 12);

            if (height == initHeight)
                if (interest is IIndependentResize)
                    height = ((IIndependentResize)interest).GetDesiredSize();

            interest.MinimumSize = new Size(((interest.Parent != null) ? interest.Parent.Width - offset : interest.Width), height);
            interest.Size = interest.MinimumSize;
        }
        
        /// <summary>
        /// Experimental. COMMENT CODE NO REMOVE!
        /// </summary>
        /// <param name="collapseState">true, collapse. false, wrap</param>
        /// <param name="interest">object of collapse</param>
        /// <param name="collapseHeight"></param>
        /// <param name="panels"></param>
        public static void IndependentCollapse(bool collapseState, Control interest, List<Panel> panels, int collapseHeight=36, params IIndependentResize[] _resizers)
        {
            int peekExpandHeight = collapseHeight;
            int testHeight = 0;

            if (collapseState)
            {
                interest.MinimumSize = new Size(interest.Width, collapseHeight);
                interest.Height = collapseHeight;

                foreach (IIndependentResize resizeTarget in _resizers)
                    resizeTarget.IndependentCollapse(collapseState);
            }
            else
            {
                foreach (Panel p in panels)
                {
                    foreach (Control c in p.Controls)
                        testHeight += (3 * c.Size.Height + 12) / p.Controls.Count;
                    peekExpandHeight = (peekExpandHeight < testHeight) ? testHeight : peekExpandHeight;
                }

                if (peekExpandHeight == collapseHeight || peekExpandHeight < interest.MinimumSize.Height)
                {
                    if (interest is IIndependentResize)
                        peekExpandHeight = ((IIndependentResize)interest).GetDesiredSize();
                    else
                        peekExpandHeight = interest.MinimumSize.Height;
                }

                //peekExpandHeight *= 2;

                interest.MinimumSize = new Size(interest.Width, peekExpandHeight);
                interest.Size = interest.MinimumSize;
                //interest.Size = new Size(interest.Width, peekExpandHeight);
            }
        }

        public static void FlipCollapseButton(Button b, bool collapseState)
        {
            b.Text = (collapseState) ? "o" : "-";
        }

        public static bool FlipCollapseButton_Check(Button b)
        {
            bool check = b.Text == "-";
            
            b.Text = (check) ? "o" : "-";

            return check;
        }

        public static List<IIndependentResize> ScanForResizers(ControlCollection container)
        {
            List<IIndependentResize> resizers = new List<IIndependentResize>();
            foreach (Control c in container)
                if (c is IIndependentResize)
                    resizers.Add((IIndependentResize)c);
            return resizers;
        }
    }
}
