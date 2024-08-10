using Raylib_cs;
using Ray = Raylib_cs.Raylib;

namespace Program
{
    public class Game
    {
        public static Bird bird = new();
        public static Pipe pipe1 = new();
        public static uint Score;
        public static bool running = true;

        public static void Start()
        {
            Ray.SetTargetFPS(60);
            Ray.InitWindow(450, 700, "Flappy Bird");
            Ray.SetWindowIcon(Ray.LoadImage("C:\\Users\\Lucas\\projects\\ConsoleApp3\\Data\\bird1.png"));
            Ray.InitAudioDevice();
            Score = 0;
        }

        public static void End()
        {
            Raylib.CloseWindow();
            Ray.CloseAudioDevice();
            bird.End();
            pipe1.End();
        }

        public static void Update()
        { 
           if (Ray.WindowShouldClose()) running = false;
            // TODO Update
            bird.Update();
            pipe1.Update();
        }

        public static void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Blue);
            // TODO Draw
            pipe1.Draw();
            bird.Draw();
            Ray.DrawText(Convert.ToString(Score), 240, 90, 75, Color.White);
            Raylib.EndDrawing();
        }

        public static Game Run()
        {
            Start();
            while (running)
            {
                Update();
                Draw();
            }
            End();
            return new Game();
        }
    }
}
