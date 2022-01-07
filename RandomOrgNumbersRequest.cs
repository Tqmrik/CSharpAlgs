using System;
using System.Net;
using System.Collections.Specialized;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace CSharpWeb
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static private string text = "";
        static private string Request(int repeat = 1, int Min = 1, int Max = 100, int numberBase = 10)
        {
            try
            {
                Uri uri = new Uri($"https://www.random.org/integers/?num={repeat}&min={Min}&max={Max}&col=1&base={numberBase}&format=plain&rnd=new&cl=w");
                var get = client.GetStringAsync(uri);
                text = get.Result.ToString().Split(new char[] { '<', '>' }, 6)[4];
            }
            catch (HttpRequestException reqExep)
            {
                Console.WriteLine(reqExep.Message);
                TryLater();
            }
            catch (TaskCanceledException taskExep)
            {
                Console.WriteLine(taskExep.Message);
                TryLater();
            }
            return text;
        }
        static void GetNumbers(int repeat = 1, int Min = 1, int Max = 100, int numberBase = 10)
        {

            Console.Write(Request(repeat, Min, Max, numberBase));
        }

        
        static void GetNumbers(string path = "Numbers.txt",int repeat = 1, int Min = 1, int Max = 100, int numberBase = 10)
        {
            File.WriteAllText("Numbers.txt", Request(repeat, Min, Max, numberBase));         
        }

        static public void TryLater()
        {
            Console.WriteLine("Program resulted in failure, try again later.");
        }

    }
}