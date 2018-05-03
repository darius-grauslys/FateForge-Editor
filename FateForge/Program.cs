/* *****************************************************************
 * FATE FORGE
 * By: Darius Grauslys
 * Version: 1.0.0
 * Date: 2/27/2018
 * ***************************************************************** 
 * 
 * Desc: Fate Forge if a Beton Quest editor that
 * takes advantage of the quest file system and
 * aims to be more user friendly than the java
 * editor.
 * 
 * 1.0.0 Design Objectives:
 * - Create Quests
 *   - Design in program.
 *   - Export single/all quests
 *   - Import single/all quests
 * - Organize objectives and events
 *   - visual programming, not name links
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FateForge
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
