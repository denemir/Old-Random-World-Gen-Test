using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Untitled_RPG_Project
{
    class World
    {
        public Rectangle[,] tiles, trees, dungeons, boats, caves;
        public Texture2D[,] textures;
        public int[,] value;
        public Random randy;
        public double treeCount, dungeonCount, caveCount, boatCount;

        public double intensity, worldSize;

        public World(double i, double wS)
        {
            intensity = i;
            worldSize = wS;
        }
        
        public void worldGen()
        {


            treeCount = 0;
            dungeonCount = 0;
            boatCount = 0;
            caveCount = 0;

            tiles = new Rectangle[(int)(320*worldSize), (int)(15*worldSize)];
            textures = new Texture2D[(int)(320 * worldSize), (int)(15 * worldSize)];
            value = new int[(int)(20 * worldSize), (int)(15 * worldSize)]; //0 = default, 1 = grass, 2 = sand, 3 = water, 4 = spruce
            trees = new Rectangle[(int)(20 * worldSize), (int)(15 * worldSize)];
            boats = new Rectangle[(int)(20 * worldSize), (int)(15 * worldSize)];
            dungeons = new Rectangle[(int)(20 * worldSize), (int)(15 * worldSize)];
            caves = new Rectangle[(int)(20 * worldSize), (int)(15 * worldSize)];

            for (int r = 0; r < (int)(20*worldSize); r++)
            {
                for (int c = 0; c < (int)(15*worldSize); c++)
                {

                    tiles[r, c] = new Rectangle((r * 64), (c * 64), 64, 64);
                }
            }

            value[0, 0] = 1;

            randy = new Random();
            for (int r = 0; r < (int)(20 * worldSize); r++)
            {
                for (int c = 1; c < (int)(15 * worldSize); c++)
                {
                    if (r == 0 && value[r, c - 1] == 1)
                    {
                        if (randy.Next(1, 1) == 1) //more grass
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 500) == 1) //sand spawn
                        {
                            value[r, c] = 2;
                        }
                        if (randy.Next(1, 20) == 1) //water spawn
                        {
                            value[r, c] = 3;
                        }
                        if (randy.Next(1, 500) == 1) //spruce spawn
                        {
                            value[r, c] = 4;
                        }
                    }
                    else if (r == 0 && value[r, c - 1] == 2)
                    {
                        if (randy.Next(1, 1) == 1) //more sand
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 20) == 1) //grass spawn
                        {
                            value[r, c] = 2;
                        }
                    }
                    else if (r == 0 && value[r, c - 1] == 3)
                    {
                        if (randy.Next(1, 1) == 1) //more water
                        {
                            value[r, c] = 3;
                        }
                        if (randy.Next(1, 100) == 1) //grass spawn
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 80) == 1) //sand spawn
                        {
                            value[r, c] = 2;
                        }
                    }
                    else if (r == 0 && value[r, c - 1] == 4)
                    {
                        if (randy.Next(1, 1) == 4) //more spruce
                        {
                            value[r, c] = 4;
                        }
                        if (randy.Next(1, 50) == 1) //grass spawn
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 20) == 1) //water spawn
                        {
                            value[r, c] = 3;
                        }
                    }
                    else if (c != 0)
                    {
                        if (r != 0)
                        {
                            value[r, c] = value[r - 1, c];
                        }
                        else value[r, c] = value[r, c - 1];
                    }
                    else if (value[r, c] == 0)
                    {
                        value[r, c] = 1;
                    }
                    else value[r, c] = 1;




                    if (r != 0 && value[r - 1, c] == 1 || value[r, c - 1] == 1)
                    {
                        if (randy.Next(1, 5) == 1)
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 20) == 1)
                        {
                            value[r, c] = 2;
                        }
                        if (randy.Next(1, 20) == 1)
                        {
                            value[r, c] = 3;
                        }
                        if (randy.Next(1, 20) == 1)
                        {
                            value[r, c] = 4;
                        }
                    }
                    else if (r != 0 && value[r - 1, c] == 2 || value[r, c - 1] == 2) //desert
                    {
                        if (randy.Next(1, 2) == 1)
                        {
                            value[r, c] = 2;
                        }
                        if (randy.Next(1, 20) == 10)
                        {
                            value[r, c] = 1;
                        }

                    }
                    else if (r != 0 && value[r - 1, c] == 3 || value[r, c - 1] == 3) //water
                    {
                        if (randy.Next(1, 2) == 1)
                        {
                            value[r, c] = 3;
                        }
                        if (randy.Next(1, 100) == 1)
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 80) == 1)
                        {
                            value[r, c] = 2;
                        }
                        //if (randy.Next(1, 30) == 1)

                    }
                    else if (r != 0 && value[r - 1, c] == 4 || value[r, c - 1] == 4) //spruce
                    {
                        if (randy.Next(1, 1) == 1)
                        {
                            value[r, c] = 4;
                        }
                        if (randy.Next(1, 50) == 1)
                        {
                            value[r, c] = 1;
                        }
                        if (randy.Next(1, 20) == 1)
                        {
                            value[r, c] = 3;
                        }
                    }
                    //else if (c != 0)
                    //{
                    //    if (r != 0)
                    //    {
                    //        value[r, c] = value[r - 1, c - 1];
                    //    }
                    //    else value[r, c] = value[r, c - 1];
                    //}
                    //else if (value[r, c] == 0)
                    //{
                    //    value[r, c] = 1;
                    //}
                    else value[r, c] = 1;










                }
             }



            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 15; c++)
                {
                    if (value[r, c] == 1)
                    {
                        if (randy.Next(1, (int)(15 * intensity)) == 1)
                        {
                            trees[r, c] = new Rectangle(64 * r, 64 * c, 16, 32);
                            treeCount++;
                        }

                    }
                    if (value[r, c] == 4)
                    {
                        if (randy.Next(1, (int)(5*intensity)) == 1)
                        {
                            trees[r, c] = new Rectangle(64 * r, 64 * c, 16, 32);
                            treeCount++;
                        }

                    }
                    if (value[r, c] == 3)
                    {
                        if (randy.Next(1, (int)(500*intensity)) == 1)
                        {
                            boats[r, c] = new Rectangle(48 * r, 32 * c, 48, 32);
                            boatCount++;
                        }

                    }
                }
            }





            for (int r = 5; r < 15; r++)
            {
                for (int c = 5; c < 10; c++)
                {
                    if (value[r, c] != 3 && value[r + 4, c] != 3)
                    {
                        if (dungeonCount < 15 && randy.Next(1, (int)(4000*intensity)) == 1)
                        {
                            dungeons[r, c] = new Rectangle(160 * r - 1, 160 * c - 1, 160, 160);
                            dungeonCount++;
                        }

                        if (caveCount < 30 && randy.Next(1, (int)(2000*intensity)) == 1)
                        {
                            caves[r, c] = new Rectangle(96 * r, 80 * c, 96, 80);
                            caveCount++;
                        }
                    }


                }
            }

            if (dungeonCount < 1)
            {
                dungeons[10, 5] = new Rectangle(2 * 64, 2 * 64, 160, 160);
                dungeonCount++;
            }




        }
    }
}
