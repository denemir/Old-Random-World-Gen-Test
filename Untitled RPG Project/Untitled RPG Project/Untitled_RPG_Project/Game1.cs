using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Untitled_RPG_Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState mouse;
        MouseState oldMouse;


        World myWorld;

        Rectangle window;
        Rectangle mou;
        SpriteFont mainFont /*font for main menu*/, gameFont /*font for general use in game*/, etcFont /*font for extras and no specific purpose*/;

        int windowSizeX, windowSizeY; //ints for setting window size

        Rectangle newGameButton, loadButton, settingsButton, exitButton;
        Texture2D pixel, minus, plus; //menu textures

        Texture2D test;
        Rectangle newC, minusR1, plusR1, minusR2, plusR2, minusR3, plusR3, backButton, okButton;
        Color newCharCo;
        string charName;

        KeyboardState oldKB;
        int Rco, Gco, Bco;

        int numOfLet;

        Vector2 nameLoc;

        Rectangle mainMenuCastle;
        Texture2D mmDungeon;
        int mmTimer;

        enum GameState
        {
            main,
            newChara,
            nameChara,
            mSettings,
            play,
            pSettings,
            lose,
            pause
        }

        bool isSettings; //checks to see if settings are open or not

        GameState state = GameState.play;

        int menuTimer; //timer for main menu intro
        int introX, introY;

        int introAccel;

        double wS, intense;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            windowSizeX = 640;
            windowSizeY = 480;
            window = new Rectangle(0, 0, windowSizeX, windowSizeY);
            graphics.PreferredBackBufferWidth = window.Width;
            graphics.PreferredBackBufferHeight = window.Height;
            // TODO: Add your initialization logic here
            graphics.ApplyChanges();

            mainMenuCastle = new Rectangle(windowSizeX / 2 - 80, windowSizeY / 2 - 130, 160, 160);

            nameLoc = new Vector2(316,375);

            menuTimer = 60;

            introY = -150;
            introAccel = 5;

            newGameButton = new Rectangle(windowSizeX / 2 - 45, windowSizeY / 2 + 50, 100, 20);
            settingsButton = new Rectangle(windowSizeX / 2 - 35, windowSizeY / 2 + 80, 60, 20);
            exitButton = new Rectangle(windowSizeX / 2 - 20, windowSizeY / 2 + 110, 40, 20);
            newC = new Rectangle(windowSizeX / 2 - 27, 125, 63, 72);

            minusR1 = new Rectangle(15, 300, 20, 20);
            plusR1 = new Rectangle(380, 300, 20, 20);
            minusR2 = new Rectangle(15, 350, 20, 20);
            plusR2 = new Rectangle(380, 350, 20, 20);
            minusR3 = new Rectangle(15, 400, 20, 20);
            plusR3 = new Rectangle(380, 400, 20, 20);

            backButton = new Rectangle(20, 15, 30, 25);
            okButton = new Rectangle(305, 440, 30, 20);

            charName = "";

            Rco = 256;
            Gco = 256;
            Bco = 256;

            oldKB = Keyboard.GetState();

            numOfLet = 0;

            wS = 1.0;
            intense = 1.0;


            myWorld = new World(intense, wS);

            if(state == GameState.play)
            {
                myWorld.worldGen();
                for (int r = 0; r < (int)(20 * wS); r++)
                {
                    for (int c = 0; c < (int)(15 * wS); c++)
                    {
                        if (myWorld.value[r, c] == 0)
                        {
                            
                            myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
                        }
                        if (myWorld.value[r, c] == 1)
                        {
                            myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
                        }
                        if (myWorld.value[r, c] == 2)
                        {
                            if (r!= 0 && myWorld.value[r-1, c] == 1)
                            {
                                myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile mml");
                            }
                            else if (r!= 19 && myWorld.value[r + 1, c] == 1)
                            {
                                myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile mmr");
                            }
                            else if(c!= 14 && myWorld.value[r, c+1] == 2)
                            {
                                myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile 1");
                            }
                            else if (c != 0 && myWorld.value[r, c- 1] == 2)
                            {
                                myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile 1");
                            }
                            else myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile mm");
                        }
                        if (myWorld.value[r, c] == 3)
                        {
                            myWorld.textures[r, c] = this.Content.Load<Texture2D>("water tile 1");
                        }
                        if (myWorld.value[r, c] == 4)
                        {
                            myWorld.textures[r, c] = this.Content.Load<Texture2D>("spruce tile 1");
                        }
                    }
                }
                    }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainFont = this.Content.Load<SpriteFont>("spritefont1");
            gameFont = this.Content.Load<SpriteFont>("spritefont2");
            etcFont = this.Content.Load<SpriteFont>("spritefont3");
            pixel = this.Content.Load<Texture2D>("pixel");
            test = this.Content.Load<Texture2D>("main d idle 1");
            plus = this.Content.Load<Texture2D>("plus");
            minus = this.Content.Load<Texture2D>("minus");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            if (state == GameState.play)
            {
                //for (int r = 0; r < (int)(20 * wS); r++)
                //{
                //    for (int c = 0; c < (int)(15 * wS); c++)
                //    {
                //        if(myWorld.tiles[r,c])
                //        if (myWorld.value[r, c] == 0)
                //        {
                //            myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
                //        }
                //        if (myWorld.value[r, c] == 1)
                //        {
                //            myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
                //        }
                //        if (myWorld.value[r, c] == 2)
                //        {
                //            myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile 1");
                //        }
                //        if (myWorld.value[r, c] == 3)
                //        {
                //            myWorld.textures[r, c] = this.Content.Load<Texture2D>("water tile 1");
                //        }
                //        if (myWorld.value[r, c] == 4)
                //        {
                //            myWorld.textures[r, c] = this.Content.Load<Texture2D>("spruce tile 1");
                //        }
                //    }
                //}
                Content.Unload();
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState kb = Keyboard.GetState();
            mouse = Mouse.GetState();
           
            mou = new Rectangle(mouse.X - 2, mouse.Y, 10, 10);

            newCharCo = new Color(Rco, Gco, Bco);

            if (state == GameState.main) //beginning of main game state
            {
                IsMouseVisible = true;
                menuTimer--;
                mmTimer++;

                if(mmTimer >= 0 && mmTimer < 5)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 1");
                }
                if (mmTimer >= 5 && mmTimer < 10)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 2");
                }
                if (mmTimer >= 10 && mmTimer < 15)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 3");
                }
                if (mmTimer >= 15 && mmTimer < 20)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 4");
                }
                if (mmTimer >= 20 && mmTimer < 25)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 5");
                }
                if (mmTimer >= 25 && mmTimer < 30)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 6");                   
                }
                if (mmTimer >= 30 && mmTimer < 35)
                {
                    mmDungeon = this.Content.Load<Texture2D>("dungeon 1 7");
                }
                if (mmTimer >= 35)
                {
                    mmTimer = 0;
                }

                if (menuTimer == 45)
                {
                    introAccel--;
                }
                if (menuTimer == 30)
                {
                    introAccel--;
                }
                if (menuTimer == 20)
                {
                    introAccel--;
                }
                if (menuTimer == 10)
                {
                    introAccel--;
                }

                if (menuTimer > 0)
                {
                    introY += introAccel;
                }

                if (mou.Intersects(newGameButton) && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    state = GameState.newChara;
                }

                if (mou.Intersects(exitButton) && mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    this.Exit();
                }

            } //end of main gamestate

            if (state == GameState.newChara) //beginning of new character game state
            {
                if (mou.Intersects(plusR1) && mouse.LeftButton == ButtonState.Pressed && Rco < 256)
                {
                    Rco++;
                }
                if (mou.Intersects(plusR2) && mouse.LeftButton == ButtonState.Pressed && Gco < 256)
                {
                    Gco++;
                }
                if (mou.Intersects(plusR3) && mouse.LeftButton == ButtonState.Pressed && Bco < 256)
                {
                    Bco++;
                }
                if (mou.Intersects(minusR1) && mouse.LeftButton == ButtonState.Pressed && Rco > 0)
                {
                    Rco--;
                }
                if (mou.Intersects(minusR2) && mouse.LeftButton == ButtonState.Pressed && Gco > 0)
                {
                    Gco--;
                }
                if (mou.Intersects(minusR3) && mouse.LeftButton == ButtonState.Pressed && Bco > 0)
                {
                    Bco--;
                }
                if (mou.Intersects(backButton) && mouse.LeftButton == ButtonState.Pressed)
                {
                    state = GameState.main;
                }
                if (mou.Intersects(okButton) && mouse.LeftButton == ButtonState.Pressed && Bco > 0 && state == GameState.newChara)
                {
                    state = GameState.nameChara;
                }

            } //end of new character game state

            if(state == GameState.nameChara)
            {
                //if (mou.Intersects(okButton) && mouse.LeftButton == ButtonState.Pressed && Bco > 0)
                //{
                //    state = GameState.play;
                //}
                if (mou.Intersects(new Rectangle(20, 50, 30, 25)) && mouse.LeftButton == ButtonState.Pressed)
                {
                    state = GameState.newChara;
                }


                if (numOfLet < 18)
                {
                    

                    if (kb.IsKeyDown(Keys.A) && oldKB.IsKeyUp(Keys.A))
                    {
                        charName = charName + "A";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.B) && oldKB.IsKeyUp(Keys.B))
                    {
                        charName = charName + "B";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.C) && oldKB.IsKeyUp(Keys.C))
                    {
                        charName = charName + "C";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.D) && oldKB.IsKeyUp(Keys.D))
                    {
                        charName = charName + "D";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.E) && oldKB.IsKeyUp(Keys.E))
                    {
                        charName = charName + "E";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.F) && oldKB.IsKeyUp(Keys.F))
                    {
                        charName = charName + "F";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.G) && oldKB.IsKeyUp(Keys.G))
                    {
                        charName = charName + "G";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.H) && oldKB.IsKeyUp(Keys.H))
                    {
                        charName = charName + "H";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.I) && oldKB.IsKeyUp(Keys.I))
                    {
                        charName = charName + "I";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.J) && oldKB.IsKeyUp(Keys.J))
                    {
                        charName = charName + "J";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.K) && oldKB.IsKeyUp(Keys.K))
                    {
                        charName = charName + "K";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.L) && oldKB.IsKeyUp(Keys.L))
                    {
                        charName = charName + "L";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.M) && oldKB.IsKeyUp(Keys.M))
                    {
                        charName = charName + "M";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.N) && oldKB.IsKeyUp(Keys.N))
                    {
                        charName = charName + "N";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.O) && oldKB.IsKeyUp(Keys.O))
                    {
                        charName = charName + "O";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.P) && oldKB.IsKeyUp(Keys.P))
                    {
                        charName = charName + "P";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.Q) && oldKB.IsKeyUp(Keys.Q))
                    {
                        charName = charName + "Q";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.R) && oldKB.IsKeyUp(Keys.R))
                    {
                        charName = charName + "R";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.S) && oldKB.IsKeyUp(Keys.S))
                    {
                        charName = charName + "S";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.T) && oldKB.IsKeyUp(Keys.T))
                    {
                        charName = charName + "T";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.U) && oldKB.IsKeyUp(Keys.U))
                    {
                        charName = charName + "U";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.V) && oldKB.IsKeyUp(Keys.V))
                    {
                        charName = charName + "V";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.W) && oldKB.IsKeyUp(Keys.W))
                    {
                        charName = charName + "W";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.X) && oldKB.IsKeyUp(Keys.X))
                    {
                        charName = charName + "X";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.Y) && oldKB.IsKeyUp(Keys.Y))
                    {
                        charName = charName + "Y";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.Z) && oldKB.IsKeyUp(Keys.Z))
                    {
                        charName = charName + "Z";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }
                    if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyUp(Keys.Space))
                    {
                        charName = charName + " ";
                        numOfLet++;
                        nameLoc.X -= 6;
                    }

                    
                }

                if (kb.IsKeyDown(Keys.Back) && oldKB.IsKeyUp(Keys.Back) && numOfLet > 0)
                {
                    charName = charName.Substring(0, charName.Length - 1);
                    numOfLet--;
                    nameLoc.X += 6;
                }
            }


            //if (state == GameState.play)
            //{
            //    for (int r = 0; r < (int)(20 * wS); r++)
            //    {
            //        for (int c = 0; c < (int)(15 * wS); c++)
            //        {
            //            if (myWorld.value[r, c] == 0)
            //            {
            //                myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
            //            }
            //            if (myWorld.value[r, c] == 1)
            //            {
            //                myWorld.textures[r, c] = this.Content.Load<Texture2D>("grass tile 1");
            //            }
            //            if (myWorld.value[r, c] == 2)
            //            {
            //                myWorld.textures[r, c] = this.Content.Load<Texture2D>("sand tile 1");
            //            }
            //            if (myWorld.value[r, c] == 3)
            //            {
            //                myWorld.textures[r, c] = this.Content.Load<Texture2D>("water tile 1");
            //            }
            //            if (myWorld.value[r, c] == 4)
            //            {
            //                myWorld.textures[r, c] = this.Content.Load<Texture2D>("spruce tile 1");
            //            }
            //        }
            //    }
            //}

            if(state == GameState.play)
            {
                if (kb.IsKeyDown(Keys.W))
                {
                    for (int r = 0; r < (int)(20 * wS); r++)
                    {
                        for (int c = 0; c < (int)(15 * wS); c++)
                        {
                            myWorld.tiles[r, c].Y += 2;
                            myWorld.trees[r, c].Y +=2 ;
                            myWorld.dungeons[r, c].Y+=2;
                            myWorld.boats[r, c].Y+=2;
                            myWorld.caves[r, c].Y+=2;
                        }
                    }
                }

                if (kb.IsKeyDown(Keys.S))
                {
                    for (int r = 0; r < (int)(20 * wS); r++)
                    {
                        for (int c = 0; c < (int)(15 * wS); c++)
                        {
                            myWorld.tiles[r, c].Y-=2;
                            myWorld.trees[r, c].Y-=2;
                            myWorld.dungeons[r, c].Y-=2;
                            myWorld.boats[r, c].Y-=2;
                            myWorld.caves[r, c].Y-=2;
                        }
                    }
                }

                if (kb.IsKeyDown(Keys.D))
                {
                    for (int r = 0; r < (int)(20 * wS); r++)
                    {
                        for (int c = 0; c < (int)(15 * wS); c++)
                        {
                            myWorld.tiles[r, c].X-=2;
                            myWorld.trees[r, c].X-=2;
                            myWorld.dungeons[r, c].X-=2;
                            myWorld.boats[r, c].X-=2;
                            myWorld.caves[r, c].X-=2;
                        }
                    }
                }

                if (kb.IsKeyDown(Keys.A))
                {
                    for (int r = 0; r < (int)(20 * wS); r++)
                    {
                        for (int c = 0; c < (int)(15 * wS); c++)
                        {
                            myWorld.tiles[r, c].X+=2;
                            myWorld.trees[r, c].X+=2;
                            myWorld.dungeons[r, c].X+=2;
                            myWorld.boats[r, c].X+=2;
                            myWorld.caves[r, c].X+=2;
                        }
                    }
                }

            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            oldMouse = mouse;
            oldKB = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
                if(state == GameState.main)
                {
                //spriteBatch.Draw(pixel, newGameButton, Color.Gray);
                spriteBatch.DrawString(mainFont, "Untitled RPG Project", new Vector2(windowSizeX / 2 - 160, introY), Color.White);
                spriteBatch.DrawString(gameFont, "New Game", new Vector2(windowSizeX / 2 - 45, windowSizeY / 2 + 50), Color.White);
                spriteBatch.DrawString(gameFont, "Settings", new Vector2(windowSizeX / 2 - 35, windowSizeY / 2 + 80), Color.White);
                spriteBatch.DrawString(gameFont, "Exit", new Vector2(windowSizeX / 2 - 20, windowSizeY / 2 + 110), Color.White);
                spriteBatch.DrawString(gameFont, "Daniel Nemirovskiy", new Vector2(10, windowSizeY-30), Color.White);
                spriteBatch.Draw(mmDungeon, mainMenuCastle, Color.White);
                
                //spriteBatch.Draw(pixel, mou, Color.Purple);
            }
                if(state == GameState.newChara)
            {
                spriteBatch.Draw(test, newC, newCharCo);
                spriteBatch.Draw(plus, plusR1, Color.White);
                spriteBatch.Draw(minus, minusR1, Color.White);
                spriteBatch.Draw(plus, plusR2, Color.White);
                spriteBatch.Draw(minus, minusR2, Color.White);
                spriteBatch.Draw(plus, plusR3, Color.White);
                spriteBatch.Draw(minus, minusR3, Color.White);
                spriteBatch.Draw(pixel, new Rectangle(310, 300, 20, 20), Color.Red);
                spriteBatch.Draw(pixel, new Rectangle(310, 350, 20, 20), Color.Green);
                spriteBatch.Draw(pixel, new Rectangle(310, 400, 20, 20), Color.Blue);
                spriteBatch.DrawString(gameFont, "" + Rco, new Vector2(420, 300), Color.Red);
                spriteBatch.DrawString(gameFont, "" + Gco, new Vector2(420, 350), Color.Green);
                spriteBatch.DrawString(gameFont, "" + Bco, new Vector2(420, 400), Color.Blue);
                spriteBatch.DrawString(gameFont, "OK", new Vector2(305, 440), Color.White);
                spriteBatch.DrawString(gameFont, "Back", new Vector2(20, 15), Color.White);
            }

                if(state == GameState.nameChara)
            {
                spriteBatch.Draw(test, newC, newCharCo);
                spriteBatch.DrawString(gameFont, "Name: ", new Vector2(295, 350), Color.White);
                spriteBatch.DrawString(gameFont, " " + charName, nameLoc, Color.White);
                spriteBatch.DrawString(gameFont, "Please type your name.", new Vector2(225, 400), Color.White);
                spriteBatch.DrawString(gameFont, "OK", new Vector2(305, 440), Color.White);
                spriteBatch.DrawString(gameFont, "Back", new Vector2(20, 50), Color.White);
            }

            if (state == GameState.play)
            {
                for (int r = 0; r < (int)(20*wS); r++)
            {
                for (int c = 0; c < (int)(15*wS); c++)
                {
                    spriteBatch.Draw(myWorld.textures[r,c], myWorld.tiles[r,c], Color.White);
                    spriteBatch.Draw(this.Content.Load<Texture2D>("oak tree"), myWorld.trees[r, c], Color.White);
                    spriteBatch.Draw(this.Content.Load<Texture2D>("dungeon 1 1"), myWorld.dungeons[r, c], Color.White);
                    spriteBatch.Draw(pixel, myWorld.boats[r, c], Color.Brown);
                    spriteBatch.Draw(this.Content.Load<Texture2D>("cave 1"), myWorld.caves[r, c], Color.White);
                }
            }
            }
            //spriteBatch.Draw(pixel, okButton, Color.Gray);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
