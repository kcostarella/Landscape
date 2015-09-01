﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;

namespace Project1
{
    public class HeightMap
    {
        public int size;
        public int max;
        private float[,] map;
        private float seed;
        private float roughness;
        private Random rand;

        //Constructor for HeightMap. DETAIL specifies the resolution of this HeightMap.
        public HeightMap(int detail)
        {
            size = (int) Math.Pow(2.0d, (double)detail) + 1;
            max = size - 1;
            map = new float[size,size];
            //Value to set the corners of map
            seed = max / 2;
            //Roughness determines steepness of terrain
            roughness = 0.8f;
            rand = new Random();
            GenerateMap();
        }

        //Fills this.map with random values using the Diamond-Square Algorithm
        private void GenerateMap()
        {
            //Sets the Corners of the Heightmap
            set(0, 0, seed);
            set(0, max, seed);
            set(max, 0, seed);
            set(max, max, seed);
            //Run the DiamondSquare Algorithm on a map of size MAX
            DiamondSquare(max);
        }

        //Runs the Diamond Square Algorithm for a SIZE x SIZE map.
        private void DiamondSquare(int size)
        {
            int half = size / 2;
            float scale = size * roughness;

            if (half < 1)
            {
                return;
            }

            //Set Center Point(s) of the Square
            for (int y = half; y < max; y = y + size)
            {
                for (int x = half; x < max; x = x + size)
                {
                    Square(x, y, half, rand.NextFloat(0.0f, 1.0f) * scale * 2 - scale );
                }
            }
            //Set Center Point(s) of the Diamond
            for (int y = 0; y <= max; y += half)
            {
                for (int x = (y + half) % size; x <= max; x += size)
                {
                    Diamond(x, y, half, rand.NextFloat(0.0f, 1.0f) * scale * 2 - scale);
                }
            }
            //Run DimondSquare on the subsize map
            DiamondSquare(size / 2);
        }

        /**Sets the center point (X,Y) of SIZE map, adjusts by averaging
         * the edges of the square */
        private void Square(int x, int y, int size, float offset)
        {
            float avg = average(new float[] {
                get(x - size, y - size), //Top Left
                get(x + size, y + size), //Top Right
                get(x - size, y - size), //Bottom Left
                get(x + size, y - size) //Bottom Right
            });
            set(x, y, avg + offset);
        }
        /**Sets the center point (X,Y) of SIZE map, adjusts by averaging
         *the edges of the diamond */
        private void Diamond(int x, int y, int size, float offset)
        {
            float avg = average(new float[] {
                get(x, y - size), //Top
                get(x - size, y), //Left
                get(x + size, y), //Right
                get(x, y + size) //Bottom
            });
            set(x, y, avg + offset);
        }

        /** Averages VALUES, if a value is -1.0, this signifies
         * that value falls outside the range of this.map */
        private float average(float[] values)
        {
            float sum = 0;
            int count = 0;
            foreach (float value in values)
            {
                if (value != -1.0f)
                {
                    sum += value;
                    count++;
                }
            }
            return sum / count;
        }
        /**Setter method for setting values in this.map, X and Y will be set
         * to VALUE */
        private void set(int x, int y, float value) 
        {
            if (!(x < 0 || x > max || y < 0 || y > max))
            {
                map[x, y] = value;
            }
        }

        /**Getter method for getting value stored at index X,Y in this.map
         * Intended to be used both in HeightMap.cs and Landscape.cs for accessing
         * values of this.map */
        public float get(int x, int y)
        {
            if (x < 0 || x > max || y < 0 || y > max)
            {
                return -1.0f;
            }
            else
            {
                return map[x, y];
            }
        }

        /** returns string representation of this.map, used for testing. */
        public override string ToString()
        {
            string str = "[";
            for (int y= 0; y <= max; y++)
            {
                for (int x = 0; x <= max; x++)
                {
                    str += get(x, y).ToString();
                    if (x != max) 
                    {
                        str += ", ";
                    }
                }
                if (y != max)
                {
                    str += "]\n[";
                }
                else
                {
                    str += "]";
                }
            }
            return str;
        }

    }
}
