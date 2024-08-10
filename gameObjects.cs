using Raylib_cs;
using System.Numerics;
using Ray = Raylib_cs.Raylib;

namespace Program
{
    public class Bird
    {
        List<Texture2D> textures;
        Vector2 Vector2;
        bool frame = false;
        float velocityY = 0.0f;
        public bool dead = false;

        public Bird()
        {
            Vector2 = new Vector2(150.0f, 350.0f);
            dead = false;
            textures = new List<Texture2D>()
            {
                Ray.LoadTexture("C:\\Users\\Lucas\\projects\\ConsoleApp3\\Data\\bird1.png"),
                Ray.LoadTexture("C:\\Users\\Lucas\\projects\\ConsoleApp3\\Data\\bird3.png")
            };
        }

        public void Draw()
        {
            Ray.DrawTextureV(textures[Convert.ToInt16(frame)], Vector2, Color.White);
        }

        public void Update()
        {
            Vector2.Y += velocityY;
            Math.Clamp(Vector2.Y, 200.0, 800.0);
            dead = false;
            if (Ray.IsKeyPressed(KeyboardKey.Space) && !dead)
            {
                velocityY -= 20f;
            }

            if (velocityY <= 20) velocityY += 0.5f;
            if (dead && Vector2.Y <= 0) velocityY += 1.5f;

            if (Vector2.Y >= 700 || Vector2.Y <= -99999)
            {
                dead = true;
                Game.running = false;
            }
            frame = !frame;

            // kills the player if they hit any of the pipes
            if (Ray.CheckCollisionCircleRec(new Vector2(Vector2.X + (textures[0].Width / 2), Vector2.Y + (textures[0].Height / 2
                )), 17, Pipe.p1Rect) || Ray.CheckCollisionCircleRec(new Vector2(Vector2.X + (textures[0].Width / 2), Vector2.Y + (textures[0].Height / 2)), 25, Pipe.p2Rect) || Ray.CheckCollisionCircleRec(new Vector2(Vector2.X + (textures[0].Width / 2), Vector2.Y + (textures[0].Height / 2
                )), 25, Pipe.wall))
            {
                dead = true;
            }
        }

        public void End()
        {
            dead = true;
            foreach (Texture2D item in textures)
            {
                Ray.UnloadTexture(item);
            }
        }
    }

    public class Pipe
    {
        Vector2 p1vector2;
        Vector2 p2vector2;
        public static Rectangle p1Rect;
        public static Rectangle p2Rect;
        public static Rectangle wall;
        Texture2D texture;
        Sound sound;

        public Pipe()
        {
            texture = Ray.LoadTexture("C:\\Users\\Lucas\\projects\\ConsoleApp3\\Data\\pipe.png");
            sound = Ray.LoadSound("C:\\Users\\Lucas\\projects\\ConsoleApp3\\Data\\sfx_point.mp3");
            Random r = new();
            int num = r.Next(-400, -60);
            p1vector2 = new Vector2(800, num);
            p1Rect = new Rectangle(p1vector2, texture.Width, texture.Height);
            p2vector2 = new Vector2(p1vector2.X, p1vector2.Y + 800);
            p2Rect = new Rectangle(p2vector2, texture.Width, texture.Height);

        }

        public void End()
        {
            Ray.UnloadTexture(texture);
        }

        public void Update()
        {
            if (p1vector2.X < -100)
            {
                // score and respawn
                Random r = new();
                int num = r.Next(-400, -60);
                p1vector2 = new Vector2(450, num);
                Ray.PlaySound(sound);
                Game.Score++;
            }
            p2vector2 = new Vector2(p1vector2.X, p1vector2.Y + 750);
            p1Rect = new Rectangle(p1vector2, texture.Width, texture.Height);
            p2Rect = new Rectangle(p2vector2, texture.Width, texture.Height);
            if (!Game.bird.dead) p1vector2.X -= 3;
            // so the player can't fly over the pipes
            wall = new Rectangle(p1vector2.X, -999999, texture.Width, 999999);
        }

        public void Draw()
        {
            Ray.DrawTextureV(texture, p1vector2, Color.White);
            Ray.DrawTextureV(texture, p2vector2, Color.White);
        }
    }
}