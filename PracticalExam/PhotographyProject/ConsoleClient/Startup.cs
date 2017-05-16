using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photography.Data;

namespace ConsoleClient
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var context = new PhotographyContext();

            var count = context.Cameras.Count();
        }
    }
}
