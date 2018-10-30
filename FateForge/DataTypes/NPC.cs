using FateForge.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.DataTypes
{
    public class NPC
    {
        private int _id;
        private string _name;
        private decimal _x;
        private decimal _y;
        private decimal _z;
        private string _world;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public decimal X { get => _x; set => _x = value; }
        public decimal Y { get => _y; set => _y = value; }
        public decimal Z { get => _z; set => _z = value; }
        public string World { get => _world; set => _world = value; }

        public NPC(
            int _id = 0,
            string _name = "",
            decimal _x = 0,
            decimal _y = 0,
            decimal _z = 0,
            string _world = ""
            )
        {
            Id = _id;
            Name = _name;
            X = _x;
            Y = _y;
            Z = _z;
            World = _world;
        }
    }
}
