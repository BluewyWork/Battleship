using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.model
{
    /// <summary>
    ///  La clase <c>Coordinate</c> es la que hará la mayor parte del trabajo del juego.
    /// </summary>
    /// <
    public abstract class Coordinate
    {
        protected int[] components;

        /// <summary>
        ///     El constructor que contendrá las coordenadas.
        /// </summary>
        /// <example>
        ///     Ejemplo de Coordenada: 
        /// <code>
        ///     (2, 4)
        /// </code>
        /// </example>
        /// 
        /// <param name="x">Numero X</param>
        /// <param name="y">Numero Y</param>
        /*public Coordinate(int x, int y)
        {
            components = new int[] { x, y };
        }*/

        protected Coordinate(int dimension)
        {
            components = new int[dimension];
        }

        /// <summary>
        /// Constructor para contar los components.
        /// </summary>
        /// <param name="c"></param>
        protected Coordinate(Coordinate c)
        {
            components = new int[c.components.Length];

            for(int i = 0; i < c.components.Length; i++)
            {
                components[i] = c.components[i];
            }
        }

        /// <summary>
        /// Comprueba que los componentes no se salgan del index.
        /// </summary>
        /// <param name="component">La coordenada que introducimos</param>
        /// <param name="value">El valor</param>
        public void Set(int component, int value)
        {
            if(component >= 0 && component < components.Length)
            {
                components[component] = value;
            }
            else
            {
                throw new ArgumentException($"Component {(component == 0 ? 'x' : component == 1 ? 'y' : 'z')} is out of bounds");
            }
        }

        public int Get(int component)
        {
            if(component >= 0 && component < components.Length)
            {
                return components[component];
            }
            else
            {
                throw new ArgumentException($"Component {(component == 0 ? 'x' : component == 1 ? 'y' : 'z')} is out of bounds");
            }
        }

        public Coordinate Add(Coordinate c)
        {
            if (c is null)
                throw new ArgumentNullException(nameof(c), "Coordinate is null");
            
            Coordinate caux = Copy();

            int tamany = (components.Length > c.components.Length) ? c.components.Length : components.Length;

            for(int i = 0; i < tamany; i++)
            {
                caux.Set(i, Get(i) + c.Get(i));
            }

            return caux;
        }

        public Coordinate Substract(Coordinate c)
        {
            if (c is null)
                throw new ArgumentNullException(nameof(c), "Coordinate is null");

            Coordinate caux = Copy();

            int tamany = (components.Length > c.components.Length) ? c.components.Length : components.Length;

            for (int i = 0; i < tamany; i++)
            {
                caux.Set(i, Get(i) - c.Get(i));
            }

            return caux;
        }

        public override bool Equals(object? obj)
        {
            if(this == obj)
                return true;
            
            if(obj == null)
                return false;

            Type refClass = this.GetType();
            Type testClass = obj.GetType();
            if (!refClass.Equals(testClass))
                return false;

            Coordinate other = (Coordinate)obj;
            for(int i = 0; i < components.Length; i++)
            {
                if (components[i] != other.components[i]) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            int hc0 = components[0].GetHashCode();
            int hc1 = components[1].GetHashCode();

            result = prime * result + hc0 * hc1;

            return result;
        }

        /*public override string? ToString()
        {
            string coord = "(";

            for(int i = 0; i < components.Length; i++)
            {
                coord += components[i].ToString();
                if(i < components.Length - 1)
                {
                    coord += ", ";
                }
            }
            coord += ")";

            return coord;
        }*/

        public override string ToString()
        {
            return "Coordinate";
        }

        /*public Coordinate Copy()
        {
            return new Coordinate(this); 
        }*/

        /*public HashSet<Coordinate> AdjacentCoordinates()
        {
            HashSet<Coordinate> adjacents = new HashSet<Coordinate>();

            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    if (x == 0 && y == 0)
                        continue;
                    else
                        adjacents.Add(new Coordinate(Get(0) + x, Get(1) + y));

            return adjacents;
        }*/

        public abstract Coordinate Copy();
        public abstract HashSet<Coordinate> AdjacentCoordinates();
    }
}
