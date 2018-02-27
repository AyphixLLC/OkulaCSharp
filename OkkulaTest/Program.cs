using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Okkula;

namespace OkkulaTest {
    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Initializing Okula Application");
            OkulaApplication.Run(new MainWindow());
        }
    }
}
